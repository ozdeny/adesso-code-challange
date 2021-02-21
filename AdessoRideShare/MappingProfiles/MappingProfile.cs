using AdessoRideShare.Core;
using AdessoRideShare.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.MappingProfiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<SaveRideDTO, Ride>()
                .ForMember(dest => dest.CreatedUserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FreeSeatCount, opt => opt.MapFrom(src => src.SeatCount))
                .ForMember(dest => dest.MaxSeatCount, opt => opt.MapFrom(src => src.SeatCount))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(d => true));

        }
    }
}
