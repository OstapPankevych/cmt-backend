using System;
using System.Net;
using System.Threading.Tasks;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers;
using Cmt.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace Cmt.WebApi.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandlerFactory _exceptionHandlerFactory;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IExceptionHandlerFactory exceptionHandlerFactory)
        {
            _next = next;
            _exceptionHandlerFactory = exceptionHandlerFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            HttpError httpError = null;
            try
            {
                await _next(context);

                var statusCode = context.Response.StatusCode;
                if (!IsSuccessStatusCode(statusCode))
                {
                    httpError = _exceptionHandlerFactory.Create(statusCode);
                }
            }
            catch (Exception ex)
            {
                httpError = _exceptionHandlerFactory.Create(ex);
            }

            if (httpError != null)
            {
                await context.WriteErrorAsync(httpError);
            }
        }

        private bool IsSuccessStatusCode(int statusCode)
            => statusCode < (int)HttpStatusCode.BadRequest;
    }
}
