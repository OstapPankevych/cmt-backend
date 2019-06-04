using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cmt.Bll.Services.Exceptions;
using Cmt.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cmt.WebApi.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(
            ILogger<ExceptionHandlingMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Critical, ex.Message, ex);
                await context.WriteErrorAsync(
                    StatusCodes.Status500InternalServerError,
                    new List<string> { CmtErrorCodes.Unknown },
                    null);
            }
        }
    }
}
