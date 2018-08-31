using System;
using AutoMapper;
using Cmt.Common.DTOs;

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
