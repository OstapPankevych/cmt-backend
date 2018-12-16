using AutoMapper;
using Cmt.Bll.DTOs.Courses;
using Cmt.Bll.DTOs.Users;
using Cmt.Dal.Entities;
using Cmt.Dal.Identities;

namespace Cmt.Bll
{
    public class BllMappingProfile: Profile
    {
        public BllMappingProfile()
        {
            CreateMap<CourseDto, CourseEntity>();

            CreateMap<UserDto, CmtIdentityUser>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
