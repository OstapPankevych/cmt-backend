using System.Linq;
using System.Threading.Tasks;
using Cmt.WebApi.ActionResults.Infrastructure;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cmt.WebApi.Infrastructure.Filters
{
    public class InvalidModelStateFilter : IAsyncActionFilter
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
                .Select(e => e.Key)
                .ToList();

            var httpError = new HttpError 
            {
                Errors = errors,
                StatusCode = StatusCodes.Status400BadRequest
            };
            context.Result = new CmtErrorResult(httpError);
        }
    }
}
