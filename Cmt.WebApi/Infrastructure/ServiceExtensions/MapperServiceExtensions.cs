using Cmt.Bll;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.ServiceExtensions
{
    public static class MapperServiceExtensions
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BllMappingProfile>();
                cfg.AddProfile<WebApiMappingProfile>();
            });

            services.AddAutoMapper();
        }
    }
}
