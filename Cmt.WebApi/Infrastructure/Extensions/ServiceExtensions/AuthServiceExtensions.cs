using Cmt.Common.Helpers;
using Cmt.WebApi.Infrastructure.Authorization.Courses.Handlers;
using Cmt.WebApi.Infrastructure.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions
{
    public static class JwtServiceExtensions
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = ConfigurationsProvider.GetJwtSettings(configuration);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = JwtHelper.GetSecurityKey(jwtSettings.SecretKey),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
        }
    }
}
