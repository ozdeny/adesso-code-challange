using AdessoRideShare.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Validators
{
    public class UpdateRideVisibilityValidator : AbstractValidator<UpdateRideVisibilityDTO>
    {
        public UpdateRideVisibilityValidator()
        {
            RuleFor(m => m.UserId).GreaterThan(0);


            //RuleFor(m => m.RideId).GreaterThan(0);

        }
    }
}
