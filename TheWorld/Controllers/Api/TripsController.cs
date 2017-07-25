using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Entities;
using TheWorld.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllTrips();
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get All Trips: {ex}");

                return BadRequest("Error occurred");
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]TripViewModel trip)
        {
            //TODO: Save to the Database
            var newTrip = Mapper.Map<Trip>(trip);

            if (ModelState.IsValid)
            {
                return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(trip));
            }

            return BadRequest(ModelState);
        }
    }
}
