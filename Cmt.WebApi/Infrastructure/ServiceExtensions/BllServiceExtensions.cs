using Cmt.Bll.Services;
using Cmt.Bll.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.ServiceExtensions
{
    public static class BllServiceExtensions
    {
        public static void ConfigureBllServices(this IServiceCollection services)
        {
            services.AddTransient<ICoursesService, CoursesService>();
            services.AddTransient<IAuthService, AuthService>();
        }
    }
}
