using Cmt.Bll.DTOs.Courses;
using Cmt.Bll.DTOs.Users;
using Cmt.Bll.Services.Interfaces;

namespace Cmt.Bll.Services
{
    public class SecurityService : ISecurityService
    {
        public bool IsOwner(CourseDto course, UserDto user)
        {
            if (!course.UpdatedBy.HasValue)
            {
                return false;
            }

            return course.UpdatedBy.Value == user.Id;
        }
    }
}
