using Cmt.Bll.DTOs.Courses;
using Cmt.Bll.DTOs.Users;

namespace Cmt.Bll.Services.Interfaces
{
    public interface ISecurityService
    {
        bool IsOwner(CourseDto course, UserDto user);
    }
}
