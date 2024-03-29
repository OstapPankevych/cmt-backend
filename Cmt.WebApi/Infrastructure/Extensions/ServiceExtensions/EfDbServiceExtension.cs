﻿using Cmt.Dal.Ef;
using Cmt.Dal.Ef.Repositories;
using Cmt.Dal.Identities;
using Cmt.Dal.Interfaces;
using Cmt.Dal.Interfaces.Repositories;
using Cmt.WebApi.Infrastructure.Constants;
using Cmt.WebApi.Infrastructure.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions
{
    public static class EfDbServiceExtension
    {
        public static void AddEfDb(
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

        public static void AddEfRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<CmtContext>>();
            services.AddScoped<UnitOfWork<CmtIdentityContext>>();

            services.AddScoped<ICourseRepository, CourseRepository>();
        }
    }
}
