using AdessoRideShare.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Services
{
    public interface IRideService
    {
        Task<Ride> CreateRide(Ride ride);

        Task<Ride> UpdateRide(Ride ride);

        IEnumerable<Ride> GetUserRides(int? departureCityId, int? arrivalCityId, int? userId);

        Task<IEnumerable<Ride>> GetRidesWithOverlappingPath(int departureCityId, int arrivalCityId);

        Task<Ride> JoinRide(Ride ride, int userId);
    }
}
