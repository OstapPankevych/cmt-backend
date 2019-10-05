using System;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.Common.Settings;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers;
using Cmt.WebApi.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions
{
    public static class WebApiServiceExtensions
    {
        public static void AddWebApi(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddTransient<IExceptionHandlerFactory, ExceptionHandlerFactory>();
            services.AddTransient<IExceptionHandler<AuthException>, AuthExceptionHandler>();
            services.AddTransient<IExceptionHandler<Exception>, ExceptionHandler>();
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
