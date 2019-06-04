using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.WebApi.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static Task WriteErrorAsync(
            this HttpContext context, 
            int statusCode, 
            IList<string> errors,
            string message)
        {
            var errorResponse = new { Errors = errors, Message = message };
            return context.WriteModelAsync(errorResponse, statusCode);
        }

        private static Task WriteModelAsync<TModel>(this HttpContext context,
            TModel model,
            int statusCode)
        {
            var result = new ObjectResult(model)
            {
                DeclaredType = typeof(TModel),
                StatusCode = statusCode
            };

            return context.ExecuteResultAsync(result);
        }

        private static Task ExecuteResultAsync<TResult>(this HttpContext context, TResult result)
            where TResult : IActionResult
        {
            var executor = context.RequestServices.GetRequiredService<IActionResultExecutor<TResult>>();

            var routeData = context.GetRouteData() ?? new RouteData();
            var actionContext = new ActionContext(context, routeData, new ActionDescriptor());

            return executor.ExecuteAsync(actionContext, result);
        }
    }
}
