using System;
using System.Threading.Tasks;
using Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers;
using Cmt.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace Cmt.WebApi.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler<Exception> _exceptionHandler;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IExceptionHandler<Exception> exceptionHandler)
        {
            _next = next;
            _exceptionHandler = exceptionHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var httpError = _exceptionHandler.Handle(ex);
                await context.Response.WriteHttpErrorAsync(httpError);
            }
        }
    }
}
