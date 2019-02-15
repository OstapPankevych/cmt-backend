using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cmt.WebApi.Infrastructure.ServiceExtensions
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

                c.OperationFilter<LastModifiedFilter>();

            });

            return services;
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Cmt API v1.0");

                c.DocumentTitle = "Title Documentation";
            });
        }

        private class LastModifiedFilter : IOperationFilter
        {
            public void Apply(Operation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<IParameter>();

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "Last-Modified",
                    In = "header",
                    Type = "string",
                    Required = false
                });
            }
        }
    }
}
