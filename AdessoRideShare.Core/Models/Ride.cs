using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Core
{
    public class Ride
    {
        public int RideId { get; set; }

        public int? DepartureCityId { get; set; }

        public int? ArrivalCityId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int MaxSeatCount { get; set; }

        public int FreeSeatCount { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedUserId { get; set; }

        public City DepartureCity { get; set; }

        public City ArrivalCity { get; set; }

        public User CreatedUser { get; set; }

        public ICollection<User> JoinedUsers { get; set; }

        public ICollection<City> RouteCities { get; set; }

    }
}
