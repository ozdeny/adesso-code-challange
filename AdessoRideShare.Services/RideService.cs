using AdessoRideShare.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Services
{
    public class RideService : IRideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPathRouteService _routeService;

        public RideService(IUnitOfWork unitOfWork, IPathRouteService routeService)
        {
            this._unitOfWork = unitOfWork;
            this._routeService = routeService;
        }
        public async Task<Ride> CreateRide(Ride ride)
        {
            var cityIds = this._routeService.FindCitiesInRoute(new City()
            {
                CityId = ride.DepartureCityId.Value
            }, new City()
            {
                CityId = ride.ArrivalCityId.Value
            });

            var cities = _unitOfWork.Cities.Find(c => cityIds.Any(id => id == c.CityId));

            ride.RouteCities = cities.ToList();

            await _unitOfWork.Rides.AddAsync(ride);

            await _unitOfWork.CommitAsync();

            return ride;
        }

        public async Task<IEnumerable<Ride>> GetRidesWithOverlappingPath(int departureCityId, int arrivalCityId)
        {
            return await _unitOfWork.Rides.GetRidesWithOverlappingPaths(departureCityId, arrivalCityId);
        }

        public IEnumerable<Ride> GetUserRides(int? departureCityId, int? arrivalCityId, int? userId)
        {
            var rides = _unitOfWork.Rides.Find(r => (!departureCityId.HasValue || r.DepartureCityId == departureCityId)
                                            && (!arrivalCityId.HasValue || r.ArrivalCityId == arrivalCityId)
                                            && (!userId.HasValue || r.CreatedUserId == userId));

            return rides;
        }

        public async Task<Ride> JoinRide(Ride ride, int userId)
        {
            var rideToUpdate = await _unitOfWork.Rides.SingleOrDefaultWithUsersAsync(r => r.RideId == ride.RideId);

            if(rideToUpdate != null)
            {
                bool alreadyJoined = rideToUpdate.JoinedUsers.Any(u => u.UserId == userId);
                if(!alreadyJoined)
                {
                    if (rideToUpdate.FreeSeatCount > 0)
                    {
                        rideToUpdate.FreeSeatCount--;

                        var user = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.UserId == userId);

                        rideToUpdate.JoinedUsers.Add(user);
                    }
                    else
                    {
                        throw new Exception("Ride capacity is full");
                    }
                } else
                {
                    throw new Exception("You already joined this ride");
                }
            }

            await _unitOfWork.CommitAsync();

            return rideToUpdate;
        }

        public async Task<Ride> UpdateRide(Ride ride)
        {
            var rideToUpdate = await _unitOfWork.Rides.SingleOrDefaultAsync(r => r.RideId == ride.RideId && r.CreatedUserId == ride.CreatedUserId);

            if(rideToUpdate != null)
            {
                rideToUpdate.IsActive = ride.IsActive;
                await _unitOfWork.CommitAsync();
            }

            return rideToUpdate;
        }
    }
}
