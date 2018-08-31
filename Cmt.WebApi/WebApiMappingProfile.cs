using System;
using AutoMapper;
using Cmt.Common.DTOs;
using Cmt.Common.Identity;
using Cmt.WebApi.Models;

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
                    opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email,
                    opts => opts.MapFrom(src => src.Email));
        }
    }
}
