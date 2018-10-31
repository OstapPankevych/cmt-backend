using System.Linq;
using System.Threading.Tasks;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Cmt.WebApi.Infrastructure.Extensions
{
    public static class ResponseExtensions
    {
        public async static Task WriteHttpExceptionAsync(
            this HttpResponse response,
            HttpException exception)
        {
            response.ContentType = "application/json";
            response.StatusCode = exception.StatusCode;

            var obj = new { errors = exception.Errors.Select(x => x), success = false };
            var json = JsonConvert.SerializeObject(obj);

            await response.WriteAsync(json);
        }
    }
}
