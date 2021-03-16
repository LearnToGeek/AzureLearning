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
            this.CreateMap<Camp, CampModel>();
        }
    }
}
