using System;
using AutoMapper;
using Cmt.Bll;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.ServiceExtensions
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
