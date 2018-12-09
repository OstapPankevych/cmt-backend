using Cmt.Bll.Services;
using Cmt.Bll.Services.Interfaces;
using Cmt.Common.Settings;
using Cmt.Dal.Ef;
using Cmt.Dal.Ef.Repositories;
using Cmt.Dal.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.ServiceExtensions
{
    public static class BllServiceExtensions
    {
        public static void ConfigureBllServices(this IServiceCollection services)
        {
            services.AddTransient<ICoursesService, CoursesService>();
        }
    }
}
