using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.DTOs
{
    public class SearchRidesDTO
    {
        public int? UserId { get; set; }

        public int? DepartureCityId { get; set; }

        public int? ArrivalCityId { get; set; }
    }
}
