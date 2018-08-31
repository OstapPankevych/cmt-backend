using System;
using System.Text;
using System.Threading.Tasks;
using Cmt.Common.Helpers;
using Cmt.WebApi.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Cmt.WebApi.ServiceExtensions
{
    public static class JwtServiceExtensions
    {
        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = ConfigurationsProvider.GetJwtSettings(configuration);

            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => {
                    options.TokenValidationParameters.ValidateIssuer = jwtSettings.ValidateIssuer;
                    options.TokenValidationParameters.ValidateAudience = jwtSettings.ValidateAudience;
                    options.TokenValidationParameters.ValidateLifetime = jwtSettings.ValidateLifetime;
                    options.TokenValidationParameters.ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey;
                    options.TokenValidationParameters.IssuerSigningKey = JwtHelper.GetSecurityKey(jwtSettings.IssuerSigningKey);

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context => 
                        {
                            var exception = context.Exception;
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static IApplicationBuilder UseJwt(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            return app;
        }
    }
}
