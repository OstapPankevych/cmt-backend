using System.IO;
using System.Threading.Tasks;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Cmt.WebApi.Infrastructure.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteHttpErrorAsync(
            this HttpResponse response,
            HttpError error)
        {
            response.ContentType = "application/json";
            response.StatusCode = error.StatusCode;
            var json = JsonConvert.SerializeObject(error.CreateBody());

            await response.WriteAsync(json);
        }

        public static async Task<string> GetBodyStringAsync(this HttpResponse response)
        {
            using (var reader = new StreamReader(response.Body))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
