using AutoMapper;
using CodeCamp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCamp.Core.Profiles
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>()
                .ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName))
                .ForMember(c => c.Address1, o => o.MapFrom(m => m.Location.Address1))
                .ForMember(c => c.Address2, o => o.MapFrom(m => m.Location.Address2))
                .ForMember(c => c.Address3, o => o.MapFrom(m => m.Location.Address3))
                .ForMember(c => c.Country, o => o.MapFrom(m => m.Location.Country));

        }
    }
}
