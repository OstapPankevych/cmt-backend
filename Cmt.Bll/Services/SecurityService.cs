using System.Security.Claims;
using Cmt.Bll.DTOs.Courses;
using Cmt.Bll.Services.Exceptions;
using Cmt.Bll.Services.Interfaces;

namespace Cmt.Bll.Services
{
    public class SecurityService : ISecurityService
    {
        public bool IsOwner(ClaimsPrincipal user, CourseDto course)
        {
            if (!course.UpdatedBy.HasValue)
            {
                throw new CmtException(CmtErrorCodes.BadData);
            }

            if (int.TryParse(GetClaimValue(user, ClaimTypes.NameIdentifier),
                out var userId))
            {
                throw new CmtException(CmtErrorCodes.Unauthorized);
            }

            return course.UpdatedBy.Value == userId;
        }

        private string GetClaimValue(ClaimsPrincipal user, string type)
        {
            return user?.FindFirstValue(type);
        }
    }
}
