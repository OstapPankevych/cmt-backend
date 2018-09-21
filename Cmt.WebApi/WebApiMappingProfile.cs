using System;
using AutoMapper;
using Cmt.Common.DTOs.Courses;
using Cmt.Common.DTOs.Users;
using Cmt.Common.Identity;
using Cmt.WebApi.Models;
using Cmt.WebApi.Models.Users;

namespace Cmt.WebApi
{
	public class WebApiMappingProfile: Profile
    {
        public WebApiMappingProfile()
        {
            CreateMap<CourseModel, CourseDto>();
            CreateMap<CourseDto, CourseModel>();

            CreateMap<NewUser, CmtIdentityUser>()
                .ForMember(dest => dest.UserName,
                    opts => opts.MapFrom(src => src.Name));

            CreateMap<User, UserDto>();
            CreateMap<Jwt, JwtDto>();
        }
    }
}
