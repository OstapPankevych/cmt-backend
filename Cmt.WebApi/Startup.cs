using Cmt.WebApi.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cmt.WebApi.Infrastructure.Filters;
using Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions;

namespace Cmt.WebApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public IConfiguration Configuration { get; }

        public Startup(
            IConfiguration configuration,
            IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddCommonServices(services);
            AddEnvServices(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            UseCommonServices(app);
            UseEnvRequestServices(app);
        }

        private void UseCommonServices(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseJwt();
            app.UseMvc();
        }

        private void AddEnvServices(IServiceCollection services)
        {
            if (_env.IsDevelopment() || _env.IsStaging())
            {
                services.AddSwaggerDocumentation();
            }
        }

        private void AddCommonServices(IServiceCollection services)
        {
            services.AddLogging(Configuration.GetSection("Logging"));
            services.AddBll();
            services.AddWebApi(Configuration);
            services.AddIdentity(Configuration);
            services.AddJwt(Configuration);
            services.AddEfDb(Configuration);
            services.AddEfRepositories();
            services.AddMapper();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ErrorResultFilter));
                options.Filters.Add(typeof(ExceptionFilter));
            });

            services.AddHttpContextAccessor();
        }

        private void UseEnvRequestServices(
            IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (_env.IsDevelopment() || _env.IsStaging())
            {
                app.UseSwaggerDocumentation();
            }
        }
    }
}
