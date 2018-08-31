using System;
using System.Threading.Tasks;
using Cmt.Common.DTOs;

namespace Cmt.Bll.Services.Interfaces
{
    public interface ICoursesService
    {
        Task<int> CreateAsync(CourseDto course);
        Task<CourseDto> GetAsync(int id);
    }
}
