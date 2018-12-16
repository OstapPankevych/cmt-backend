using System;
using System.Net;
using System.Threading.Tasks;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Cmt.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cmt.WebApi.Infrastructure.Attributes
{
    public class RequireHttpHeaderFilterAttribute : IAsyncActionFilter
    {
        private readonly string _header;
        private readonly Type _headerType;

        public RequireHttpHeaderFilterAttribute(string header, Type type = null)
        {
            _header = header;
            _headerType = type;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool tryGetValue = context.HttpContext.Request.Headers
                .TryGetValue(_header, out var value);

            //if (tryGetValue)
            //{
            //    //var httpException = new HttpException
            //    //{
            //    //    Errors = new List<>
            //    //    StatusCode = StatusCodes.Status400BadRequest
            //    //};

            //    await context.HttpContext.Response
            //        .WriteExceptionAsync(httpException);

            //    context.Result = new ContentResult
            //    {
            //        StatusCode = (int)HttpStatusCode.PreconditionRequired
            //    };
            //}

            await next();
        }
    }
}
