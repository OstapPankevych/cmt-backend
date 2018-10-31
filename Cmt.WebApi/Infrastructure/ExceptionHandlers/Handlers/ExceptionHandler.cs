using System;
using Microsoft.AspNetCore.Http;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class ExceptionHandler : IExceptionHandler<Exception>
    {
        public HttpException Handle(Exception ex)
        {
            // Logging...
            return new HttpException
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
