using System;
using System.Collections.Generic;
using Cmt.Bll.Services.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class ExceptionHandler : IExceptionHandler<Exception>
    {
        public HttpError Handle(Exception ex)
        {
            return ex is CmtException
                ? HandleCmtException((CmtException)ex)
                : new HttpError
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Errors = new List<string> { CmtErrorCodes.Unknown }
                    };
        }

        private HttpError HandleCmtException(CmtException cmtException)
        {
            int statusCode;
            var code = cmtException.Error.Code;
            switch (code)
            {
                case CmtErrorCodes.NotFound:
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                case CmtErrorCodes.LastModified:
                    statusCode = StatusCodes.Status412PreconditionFailed;
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var errorCode = statusCode == StatusCodes.Status500InternalServerError
                ? CmtErrorCodes.Unknown
                : code;

            return new HttpError
            {
                StatusCode = statusCode,
                Errors = new List<string> { errorCode }
            };
        }
    }
}
