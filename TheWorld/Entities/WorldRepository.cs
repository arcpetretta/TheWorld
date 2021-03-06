﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Entities
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All Trips from Database");
            return _context.Trips.ToList();
        }

        public IEnumerable<Trip> GetTripsByUserName(string name)
        {
            _logger.LogInformation($"Retrieving Trips for User: {name}");
            return _context
                .Trips
                .Include(t => t.Stops)
                .Where(t => t.UserName == name)
                .ToList();
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .Where(t => t.Name.ToLower() == tripName.ToLower())
                .FirstOrDefault();
        }

        public void AddStop(string tripName, Stop newStop, string username)
        {
            var trip = GetUserTripByName(tripName, username);

            if(trip != null)
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }

        public Trip GetUserTripByName(string tripName, string username)
        {
            _logger.LogInformation($"Getting Trip: \"{tripName}\" & stops for User: {username}.");
            return _context.Trips
                .Include(t => t.Stops)
                .Where(t => t.Name.ToLower() == tripName.ToLower() && t.UserName == username)
                .FirstOrDefault();
        }
    }
}
