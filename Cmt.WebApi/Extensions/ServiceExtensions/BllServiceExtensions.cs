using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll;
using Cmt.Bll.Services;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.Bll.Services.Interfaces;
using Cmt.Common.Identity;
using Cmt.Common.Settings;
using Cmt.Dal.Entities;
using Cmt.Dal.Entities.Identity;
using Cmt.Dal.Repositories;
using Cmt.Dal.Repositories.Interfaces;
using Cmt.WebApi.ExceptionHandlers;
using Cmt.WebApi.ExceptionHandlers.Handlers;
using Cmt.WebApi.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Cmt.WebApi.ServiceExtensions
{
    public static class BllServiceExtensions
    {
        public static void ConfigureCmtDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CmtContext>(options => 
                options.UseSqlServer(ConfigurationsProvider.GetConnectionString(Constants.CmtDatabaseConfigurationSection, configuration)));
        }

        public static void ConfigureBllServices(this IServiceCollection services)
        {
            services.AddTransient<ICoursesService, CoursesService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICoursesRepository, CoursesRepository>();
        }
    }
}
