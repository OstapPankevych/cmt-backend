using System;
using Cmt.Bll.Services.Exceptions;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.Common.Settings;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers;
using Cmt.WebApi.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions
{
    public static class WebApiServiceExtensions
    {
        public static void AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExceptionHandlerFactory, ExceptionHandlerFactory>();
            services.AddTransient<IExceptionHandler<Exception>, ExceptionHandler>();
            services.AddTransient<IExceptionHandler<CmtException>, CmtExceptionHandler>();
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
