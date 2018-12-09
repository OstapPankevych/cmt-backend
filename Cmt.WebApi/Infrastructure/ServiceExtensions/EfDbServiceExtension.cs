using Cmt.Bll.Services;
using Cmt.Bll.Services.Interfaces;
using Cmt.Common.Settings;
using Cmt.Dal.Ef;
using Cmt.Dal.Ef.Repositories;
using Cmt.Dal.Entities.Identities;
using Cmt.Dal.Interfaces.Repositories;
using Cmt.WebApi.Infrastructure.Constants;
using Cmt.WebApi.Infrastructure.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.ServiceExtensions
{
    public static class EfDbServiceExtension
    {
        public static void ConfigureEfDb(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<CmtContext>(options =>
                options.UseSqlServer(ConfigurationsProvider.GetConnectionString(
                    WebApiConstants.CmtDatabaseConfigurationSection, configuration)));

            services.AddDbContext<CmtIdentityContext>(options =>
                options.UseSqlServer(ConfigurationsProvider.GetConnectionString(
                    WebApiConstants.CmtIdentityDatabaseConfigurationSection, configuration)));

            services.AddIdentity<CmtIdentityUser, CmtIdentityRole>()
                .AddEntityFrameworkStores<CmtIdentityContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureEfRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<CmtContext>>();
            services.AddScoped<UnitOfWork<CmtIdentityContext>>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            services.AddTransient<IAuthService, AuthService>(
                provider => new AuthService(
                    provider.GetService<UnitOfWork<CmtIdentityContext>>(),
                    provider.GetService<SignInManager<CmtIdentityUser>>(),
                    provider.GetService<UserManager<CmtIdentityUser>>(),
                    provider.GetService<RoleManager<CmtIdentityRole>>(),
                    provider.GetService<AuthSettings>()));
        }
    }
}
