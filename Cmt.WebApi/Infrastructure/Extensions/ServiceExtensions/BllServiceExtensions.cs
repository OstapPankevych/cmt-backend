using AutoMapper;
using Cmt.Bll.Services;
using Cmt.Bll.Services.Interfaces;
using Cmt.Common.Settings;
using Cmt.Dal.Ef;
using Cmt.Dal.Ef.Repositories;
using Cmt.Dal.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions
{
    public static class BllServiceExtensions
    {
        public static void AddBll(this IServiceCollection services)
        {
            services.AddTransient<ICoursesService, CoursesService>();
            services.AddTransient<ISecurityService, SecurityService>();

            services.AddTransient<IAuthService, AuthService>(
               provider => new AuthService(
                   provider.GetService<IMapper>(),
                   provider.GetService<UnitOfWork<CmtIdentityContext>>(),
                   provider.GetService<SignInManager<CmtIdentityUser>>(),
                   provider.GetService<UserManager<CmtIdentityUser>>(),
                   provider.GetService<RoleManager<CmtIdentityRole>>(),
                   provider.GetService<AuthSettings>()));
        }
    }
}
