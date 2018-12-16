using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;

namespace Cmt.WebApi.Infrastructure.ActionResults
{
    public class ErrorResult : IActionResult
    {
        private readonly HttpError _httpException;

        public ErrorResult(HttpError httpException)
        {
            _httpException = httpException;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(_httpException.CreateBody())
            {
                StatusCode = _httpException.StatusCode
            };

            await result.ExecuteResultAsync(context);
        }
    }
}
