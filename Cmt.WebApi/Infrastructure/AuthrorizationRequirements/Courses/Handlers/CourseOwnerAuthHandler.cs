using System.Security.Claims;
using System.Threading.Tasks;
using Cmt.Common.DTOs.Courses;
using Cmt.WebApi.Infrastructure.AuthrorizationRequirements.Course;
using Microsoft.AspNetCore.Authorization;

namespace Cmt.WebApi.Infrastructure.AuthrorizationRequirements.Courses.Handlers
{
    public class CourseOwnerAuthHandler : AuthorizationHandler<CourseOwnerAuthRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            CourseOwnerAuthRequirement requirement)
        {
            var course = context.Resource as CourseDto;
            var nameIdentifierClaim = context.User
                .FindFirst(x => x.Type == ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim != null
                && course.UpdatedBy.ToString() == nameIdentifierClaim.Value)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
