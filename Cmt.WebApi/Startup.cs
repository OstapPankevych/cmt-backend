using Cmt.WebApi.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cmt.WebApi.Infrastructure.ServiceExtensions;
using Cmt.WebApi.Infrastructure.Filters;
using Microsoft.AspNetCore.Http;

namespace Cmt.WebApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(
            IConfiguration configuration,
            IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);

            if (_env.IsDevelopment() || _env.IsStaging())
            {
                services.AddSwaggerDocumentation();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ConfigureCommon(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseSwaggerDocumentation();
            }
        }

        private void ConfigureCommonServices(IServiceCollection services)
        {
            services.ConfigureBllServices();
            services.ConfigureWebApiServices(Configuration);
            services.ConfigureIdentityServer(Configuration);
            services.ConfigureJwt(Configuration);
            services.ConfigureEfDb(Configuration);
            services.ConfigureEfRepositories();
            services.ConfigureMapper();

            services.AddCors();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(InvalidModelStateFilter));
                options.Filters.Add(typeof(ExceptionFilter));
            });

            services.AddHttpContextAccessor();
        }

        private void ConfigureCommon(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            app.UseJwt();
            app.UseMvc();
        }
    }
}
