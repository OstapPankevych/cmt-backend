using System.Security.Claims;
using System.Threading.Tasks;
using Cmt.Bll.DTOs.Courses;
using Microsoft.AspNetCore.Authorization;

namespace Cmt.WebApi.Infrastructure.Authorization.Courses.Handlers
{
    public class CourseOwnerAuthHandler : AuthorizationHandler<CourseOwnerRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CourseOwnerRequirement requirement)
        {
            var course = context.Resource as CourseDto;
            var userId = GetClaimValue(context.User, ClaimTypes.NameIdentifier);

            if (userId == course.CreatedBy.ToString())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Fail(context);
        }

        private string GetClaimValue(ClaimsPrincipal user, string type)
        {
            return user.FindFirstValue(type);
        }

        private Task Fail(AuthorizationHandlerContext context)
        {
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
