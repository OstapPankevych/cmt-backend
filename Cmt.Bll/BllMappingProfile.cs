using AutoMapper;
using Cmt.Common.DTOs.Courses;
using Cmt.Dal.Entities;

namespace Cmt.Bll
{
    public class BllMappingProfile: Profile
    {
        public BllMappingProfile()
        {
            CreateMap<CourseDto, CourseEntity>();
            CreateMap<CourseEntity, CourseDto>();
        }
    }
}
