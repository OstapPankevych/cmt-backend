﻿using System.Security.Claims;
using Cmt.Common.Helpers;
using Cmt.WebApi.Infrastructure.AuthorizationRequirements.Courses;
using Cmt.WebApi.Infrastructure.AuthorizationRequirements.Courses.Handlers;
using Cmt.WebApi.Infrastructure.Constants;
using Cmt.WebApi.Infrastructure.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions
{
    public static class JwtServiceExtensions
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = ConfigurationsProvider.GetJwtSettings(configuration);

            services
                .AddAuthorization(opts => {
                    opts.AddPolicy(Policies.CourseOwner, policy => {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(ClaimTypes.NameIdentifier);
                        policy.AddRequirements(new CourseOwnerRequirement());
                    });
                })
                .AddAuthentication()
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

            services.AddTransient<IAuthorizationHandler, CourseOwnerAuthHandler>();
        }

        public static void UseJwt(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
