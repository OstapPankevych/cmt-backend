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
            // Logging...
            return new HttpError
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Errors = new List<string> { ErrorCodes.Unknown }
            };
        }
    }
}
