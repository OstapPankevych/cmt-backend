using System;
using System.Linq;
using System.Threading.Tasks;
using Cmt.WebApi.ExceptionHandlers;
using Cmt.WebApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cmt.WebApi.Filters
{
    public class InvalidModelStateFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                await next();
                return;
            }

            var errors = context.ModelState
                .Where(e => e.Value.Errors.Count > 0)
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
