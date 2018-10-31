using Cmt.WebApi.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cmt.WebApi.Infrastructure.ServiceExtensions;
using Cmt.WebApi.Infrastructure.Filters;

namespace Cmt.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureBllServices();
            services.ConfigureWebApiServices(Configuration);
            services.ConfigureIdentityServer(Configuration);
            services.ConfigureJwt(Configuration);
            services.ConfigureCmtDb(Configuration);
            services.ConfigureMapper();

            services.AddSwaggerDocumentation();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(InvalidModelStateFilterAttribute));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseJwt();
            app.UseSwaggerDocumentation();

            app.UseMvc();
        }
    }
}
