using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Core
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public ICollection<Ride> CreatedRides { get; set; }

        public ICollection<Ride> JoinedRides { get; set; }
    }
}
