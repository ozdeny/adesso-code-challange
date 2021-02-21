using System;
using System.Collections.Generic;

namespace AdessoRideShare.Core
{
    public class City
    {
        public int CityId { get; set; }

        public string Name { get; set; }

        public ICollection<Ride> Rides;
    }
}
