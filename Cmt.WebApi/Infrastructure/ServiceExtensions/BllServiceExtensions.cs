using Cmt.Bll.Services;
using Cmt.Bll.Services.Interfaces;
using Cmt.Dal.Entities;
using Cmt.Dal.Repositories;
using Cmt.Dal.Repositories.Interfaces;
using Cmt.WebApi.Infrastructure.Constants;
using Cmt.WebApi.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.ServiceExtensions
{
    public static class BllServiceExtensions
    {
        public static void ConfigureCmtDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CmtContext>(options => 
                options.UseSqlServer(ConfigurationsProvider.GetConnectionString(
                    WebApiConstants.CmtDatabaseConfigurationSection, configuration)));
        }

        public static void ConfigureBllServices(this IServiceCollection services)
        {
            services.AddTransient<ICoursesService, CoursesService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICoursesRepository, CoursesRepository>();
        }
    }
}
