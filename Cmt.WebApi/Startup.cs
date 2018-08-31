using Cmt.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cmt.WebApi.Filters;
using Cmt.WebApi.ServiceExtensions;
using Cmt.WebApi.Extensions.ServiceExtensions;

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

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(InvalidModelStateFilterAttribute));
            });

            services.ConfigureSwaggerUi();
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
            app.UseMvc();

            app.UseSwaggerUi();
        }
    }
}
