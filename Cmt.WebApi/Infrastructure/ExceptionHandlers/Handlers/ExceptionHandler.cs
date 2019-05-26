using System;
using System.Collections.Generic;
using Cmt.Bll.Services.Exceptions;
using Cmt.WebApi.Infrastructure.HttpErrors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class ExceptionHandler : IExceptionHandler<Exception>
    {
        protected readonly ILogger<ExceptionHandler> Logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            Logger = logger;
        }

        public virtual HttpError Handle(Exception ex)
        {
            var httpError = CreateHttpError(
                        StatusCodes.Status500InternalServerError,
                        new List<string> { CmtErrorCodes.Unknown });
            Log(LogLevel.Error, ex, httpError);
            return httpError;
        }

        public virtual HttpError Handle(int httpStatusCode)
            => CreateHttpError(httpStatusCode);

        protected HttpError CreateHttpError(int statusCode, List<string> errors)
            => new HttpError
            {
                StatusCode = statusCode,
                Errors = errors
            };

        protected HttpError CreateHttpError(int statusCode, string error = null)
            => CreateHttpError(statusCode, new List<string> { error });

        protected void Log(LogLevel logLevel, Exception ex, HttpError httpError)
        {
            Logger.Log(logLevel, ex.Message, httpError);
        }
    }
}
