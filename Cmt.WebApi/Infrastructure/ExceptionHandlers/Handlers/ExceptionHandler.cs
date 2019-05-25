using System;
using System.Collections.Generic;
using Cmt.Bll.Services.Exceptions;
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

        public HttpError Handle(Exception ex)
        {
            switch (ex)
            {
                case CmtException cmtException:
                    return HandleCmtException(cmtException);
                default:
                    var httpError = CreateHttpError(
                        StatusCodes.Status500InternalServerError, 
                        new List<string> { CmtErrorCodes.Unknown });
                    Log(LogLevel.Error, ex, httpError);
                    return httpError;
            }
        }

        protected HttpError CreateHttpError(int statusCode, List<string> errors)
            => new HttpError
            {
                StatusCode = statusCode,
                Errors = errors
            };

        protected HttpError CreateHttpError(int statusCode, string error)
            => CreateHttpError(statusCode, new List<string> { error });

        protected void Log(LogLevel logLevel, Exception ex, HttpError httpError)
        {
            Logger.Log(logLevel, ex.Message, httpError);
        }

        private HttpError HandleCmtException(CmtException cmtException)
        {
            var code = cmtException.Error.Code;
            HttpError httpError;
            switch (code)
            {
                case CmtErrorCodes.NotFound:
                    httpError = CreateHttpError(StatusCodes.Status404NotFound, code);
                    Log(LogLevel.Debug, cmtException, httpError);
                    break;
                case CmtErrorCodes.LastModified:
                    httpError = CreateHttpError(StatusCodes.Status412PreconditionFailed, code);
                    Log(LogLevel.Debug, cmtException, httpError);
                    break;
                default:
                    httpError = CreateHttpError(StatusCodes.Status500InternalServerError, CmtErrorCodes.Unknown);
                    Log(LogLevel.Error, cmtException, httpError);
                    break;
            }

            return httpError;
        }
    }
}
