using AutoMapper;
using Cmt.Bll.DTOs.Courses;
using Cmt.Bll.DTOs.Users;
using Cmt.WebApi.Models.Courses;
using Cmt.WebApi.Models.Users;

namespace Cmt.WebApi.Infrastructure
{
	public class WebApiMappingProfile: Profile
    {
        public WebApiMappingProfile()
        {
            CreateMap<Course, CourseDto>()
                .ReverseMap();

            CreateMap<NewUser, UserDto>();
            CreateMap<User, UserDto>();
            CreateMap<Jwt, JwtDto>();
        }
    }
}
