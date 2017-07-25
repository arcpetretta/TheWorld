using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Entities;
using TheWorld.ViewModels;
using AutoMapper;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository)
        {
            _repository = repository;
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
                // TODO Logging

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
