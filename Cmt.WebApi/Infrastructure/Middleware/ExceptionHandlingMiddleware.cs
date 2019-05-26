using System;
using System.Threading.Tasks;
using Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers;
using Cmt.WebApi.Infrastructure.Extensions;
using Cmt.WebApi.Infrastructure.HttpErrors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cmt.WebApi.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler<Exception> _exceptionHandler;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(
            ILogger<ExceptionHandlingMiddleware> logger,
            RequestDelegate next,
            IExceptionHandler<Exception> exceptionHandler)
        {
            _logger = logger;
            _next = next;
            _exceptionHandler = exceptionHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            HttpError httpError = null;
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, context.Request);
                httpError = _exceptionHandler.Handle(ex);
                await context.WriteErrorAsync(httpError);
            }
        }
    }
}
