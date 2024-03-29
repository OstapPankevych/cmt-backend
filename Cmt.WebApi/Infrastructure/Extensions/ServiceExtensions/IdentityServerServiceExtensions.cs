﻿using System;
using Cmt.WebApi.Infrastructure.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions
{
    public static class IdentityServerServiceExtensions
    {
        public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var passwordSettings = ConfigurationsProvider.GetPasswordSettings(configuration);
            var lockoutSettings = ConfigurationsProvider.GetLockoutSettings(configuration);
            var userSettings = ConfigurationsProvider.GetUserSettings(configuration);

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = passwordSettings.RequireDigit;
                options.Password.RequireDigit = passwordSettings.RequireDigit;
                options.Password.RequiredLength = passwordSettings.RequiredLength;
                options.Password.RequireNonAlphanumeric = passwordSettings.RequireNonAlphanumeric;
                options.Password.RequireUppercase = passwordSettings.RequireUppercase;
                options.Password.RequireLowercase = passwordSettings.RequireLowercase;
                options.Password.RequiredUniqueChars = passwordSettings.RequiredUniqueChars;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutSettings.DefaultLockoutMinutes);
                options.Lockout.MaxFailedAccessAttempts = lockoutSettings.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = lockoutSettings.AllowedForNewUsers;

                options.User.RequireUniqueEmail = userSettings.RequireUniqueEmail;
            });
        }
    }
}
