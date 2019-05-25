using Cmt.Bll;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions
{
    public static class MapperServiceExtensions
    {
        public static void AddMapper(this IServiceCollection services)
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
