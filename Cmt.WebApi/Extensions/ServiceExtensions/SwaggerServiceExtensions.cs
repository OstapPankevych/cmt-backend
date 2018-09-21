using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Cmt.WebApi.Extensions.ServiceExtensions
{
    public static class SwaggerServiceExtensions
    {
        private const string Bearer = "Bearer";

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "Cmt API v1.0", Version = "v1.0" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {Bearer, new string[] { }},
                };

                c.AddSecurityDefinition(Bearer, new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Cmt API v1.0");

                c.DocumentTitle = "Title Documentation";
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
