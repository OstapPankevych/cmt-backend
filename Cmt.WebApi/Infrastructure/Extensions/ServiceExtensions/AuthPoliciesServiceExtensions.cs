using System.Security.Claims;
using Cmt.WebApi.Infrastructure.Authorization.Courses;
using Cmt.WebApi.Infrastructure.Authorization.Courses.Handlers;
using Cmt.WebApi.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions
{
    public static class AuthPoliciesServiceExtensions
    {
        public static void AddPolicies(this IServiceCollection services)
        {
            services
                .AddAuthorization(AddPolicies);

            services.AddTransient<IAuthorizationHandler, CourseOwnerAuthHandler>();
        }

        private static void AddPolicies(AuthorizationOptions opts)
        {
            opts.AddPolicy(Policies.CourseOwner, policy => {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(ClaimTypes.NameIdentifier);
                policy.AddRequirements(new CourseOwnerRequirement());
            });

            opts.AddPolicy(Policies.CourseCreator, policy => {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(ClaimTypes.NameIdentifier);
            });
        }
    }
}
