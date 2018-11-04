using AutoMapper;
using Cmt.Common.DTOs.Courses;
using Cmt.Dal.Entities;

namespace Cmt.Common.Mapping
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<CourseDto, CourseEntity>();
        }
    }
}
