﻿using System;
using Microsoft.AspNetCore.Http;

namespace Cmt.WebApi.ExceptionHandlers.Handlers
{
    public class ExceptionHandler : IExceptionHandler<Exception>
    {
        public HttpException Handle(Exception ex)
        {
            return new HttpException
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
