using AutoMapper;
using Music_2.Data.Entities;
using Music_2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserViewModel>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(x => x.UserName));
            CreateMap<UserViewModel, AppUser>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(x => x.FirstName)); ;
        }
    }
}
