using System.Collections.Generic;

namespace TheWorld.Entities
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
    }
}