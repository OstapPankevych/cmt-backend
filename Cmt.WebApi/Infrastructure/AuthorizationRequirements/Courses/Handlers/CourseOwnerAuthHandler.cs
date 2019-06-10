using System.Security.Claims;
using System.Threading.Tasks;
using Cmt.Bll.DTOs.Courses;
using Cmt.Bll.DTOs.Users;
using Cmt.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Cmt.WebApi.Infrastructure.AuthorizationRequirements.Courses.Handlers
{
    public class CourseOwnerAuthHandler : AuthorizationHandler<CourseOwnerAuthRequirement>
    {
        private readonly ISecurityService _securityService;

        public CourseOwnerAuthHandler(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            CourseOwnerAuthRequirement requirement)
        {
            var course = context.Resource as CourseDto;

            if (!_securityService.IsOwner(course, context.User))
            {
                return Fail(context);
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        private Task Fail(AuthorizationHandlerContext context)
        {
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
