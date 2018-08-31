using System;
using System.Threading.Tasks;
using Cmt.WebApi.ExceptionHandlers;
using Microsoft.AspNetCore.Http;
using Cmt.WebApi.Extensions;

namespace Cmt.WebApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IExceptionHandlerFactory _exceptionHandlerFactory;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IExceptionHandlerFactory exceptionHandlerFactory)
        {
            this.next = next;
            _exceptionHandlerFactory = exceptionHandlerFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var httpException = _exceptionHandlerFactory.Create(ex);
                await context.Response.WriteHttpExceptionAsync(httpException);
            }
        }
    }
}
