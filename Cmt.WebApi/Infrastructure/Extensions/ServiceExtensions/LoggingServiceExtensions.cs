using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.Extensions.ServiceExtensions
{
    public static class LoggingServiceExtensions
    {
        public static void AddLogging(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddLogging(logging => 
            {
                logging.AddConfiguration(configuration);
                logging.AddConsole();
                logging.AddDebug();
            });
        }
    }
}
