using AdessoRideShare.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Validators
{
    public class SaveRideValidator: AbstractValidator<SaveRideDTO>
    {
        public SaveRideValidator()
        {
            RuleFor(m => m.UserId).GreaterThan(0);

            RuleFor(m => m.ArrivalCityId).GreaterThan(0);

            RuleFor(m => m.DepartureCityId).GreaterThan(0);

            RuleFor(m => m.Date).GreaterThan(DateTime.UtcNow);

        }
    }
}
