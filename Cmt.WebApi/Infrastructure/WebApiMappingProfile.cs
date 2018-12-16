using AutoMapper;
using Cmt.Bll.DTOs.Courses;
using Cmt.Bll.DTOs.Users;
using Cmt.WebApi.Models;
using Cmt.WebApi.Models.Users;

namespace Cmt.WebApi.Infrastructure
{
	public class WebApiMappingProfile: Profile
    {
        public WebApiMappingProfile()
        {
            CreateMap<CourseModel, CourseDto>()
                .ReverseMap();

            CreateMap<NewUser, UserDto>();
            CreateMap<User, UserDto>();
            CreateMap<Jwt, JwtDto>();
        }
    }
}
