using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.DTOs
{
    public class SaveRideDTO
    {
        public int? DepartureCityId { get; set; }

        public int? ArrivalCityId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int SeatCount { get; set; }
    }
}
