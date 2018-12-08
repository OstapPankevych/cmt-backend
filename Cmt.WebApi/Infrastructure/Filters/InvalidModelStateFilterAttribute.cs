using System.Linq;
using System.Threading.Tasks;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Cmt.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cmt.WebApi.Infrastructure.Filters
{
    public class InvalidModelStateFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                await next();
                return;
            }

            var errors = context.ModelState
                .Where(e => e.Value.Errors.Any())
                .Select(e => e.Key);

            var httpException = new HttpException 
            {
                Errors = errors,
                StatusCode = StatusCodes.Status400BadRequest
            };

            await context.HttpContext.Response.WriteHttpExceptionAsync(httpException);
        }
    }
}
