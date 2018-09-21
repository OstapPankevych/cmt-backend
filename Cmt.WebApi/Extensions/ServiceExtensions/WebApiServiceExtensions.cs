using System;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.Common.Settings;
using Cmt.WebApi.ExceptionHandlers;
using Cmt.WebApi.ExceptionHandlers.Handlers;
using Cmt.WebApi.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.ServiceExtensions
{
    public static class WebApiServiceExtensions
    {
        public static void ConfigureWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExceptionHandlerFactory, ExceptionHandlerFactory>();
            services.AddTransient<IExceptionHandler<Exception>, ExceptionHandler>();
            services.AddTransient<IExceptionHandler<AuthException>, AuthExceptionHandler>();

            services.AddSingleton(GetAuthSettings(configuration));
        }

        private static AuthSettings GetAuthSettings(IConfiguration configuration)
        {
            var jwtSettings = ConfigurationsProvider.GetJwtSettings(configuration);
            var passwordSettings = ConfigurationsProvider.GetPasswordSettings(configuration);

            return new AuthSettings
            {
                JwtSecurityKey = jwtSettings.SecretKey,
                JwtExpirationTimeMinutes = jwtSettings.UserExpirationTimeMinutes
            };
        }
    }
}
