using System;
using System.Collections.Generic;
using System.Net;
using Cmt.Bll.Services.Exceptions;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers
{
    public class ExceptionHandlerFactory : IExceptionHandlerFactory
    {
        private readonly IExceptionHandler<Exception> _exceptionHandler;
        private readonly IExceptionHandler<AuthException> _authExceptionHandler;

        public ExceptionHandlerFactory(
            IExceptionHandler<Exception> exceptionHandler,
            IExceptionHandler<AuthException> authExceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
            _authExceptionHandler = authExceptionHandler;
        }

        public HttpError Create(Exception ex)
        {
            if (ex is AuthException)
            {
                return _authExceptionHandler.Handle((AuthException)ex);
            }

            return _exceptionHandler.Handle(ex);
        }

        public HttpError Create(int httpStatusCode)
        {
            var httpError = new HttpError
            {
                StatusCode = httpStatusCode,
                Errors = new List<string> { GetErrorName(httpStatusCode) }
            };

            return httpError;
        }

        private string GetErrorName(int httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case (int)HttpStatusCode.NotFound:
                    return CmtErrorCodes.NotFound;
                case (int)HttpStatusCode.InternalServerError:
                    return CmtErrorCodes.Unknown;
                default:
                    {
                        if (Enum.IsDefined(typeof(HttpStatusCode), httpStatusCode))
                        {
                            return ((HttpStatusCode)httpStatusCode).ToString();
                        }

                        return $"{httpStatusCode}Error";
                    }
            }
        }
    }
}
