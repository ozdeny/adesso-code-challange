using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.DTOs
{
    public class UpdateRideVisibilityDTO
    {
        public int UserId { get; set; }

        public bool IsActive { get; set; }

    }
}
