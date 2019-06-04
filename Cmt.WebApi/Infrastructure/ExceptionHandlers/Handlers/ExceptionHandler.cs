﻿using System;
using Cmt.Bll.Services.Exceptions;
using Cmt.WebApi.ActionResults.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class ExceptionHandler:
        IExceptionHandler<Exception>
    {
        protected readonly ILogger<ExceptionHandler> Logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            Logger = logger; 
        }

        public CmtErrorResult Handle(Exception ex)
        {
            return ex is CmtException cmtException
                ? HandleCmtException(cmtException)
                : HandleException(ex);
        }

        protected void Log(LogLevel logLevel, Exception ex)
        {
            Logger.Log(logLevel, ex.Message);
        }

        private CmtErrorResult HandleException(Exception ex)
        {
            Log(LogLevel.Error, ex);
            return new CmtErrorResult(
                StatusCodes.Status500InternalServerError,
                CmtErrorCodes.Unknown);
        }

        private CmtErrorResult HandleCmtException(CmtException ex)
        {
            switch (ex.Error.Code)
            {
                case CmtErrorCodes.NotFound:
                    Log(LogLevel.Debug, ex);
                    return new CmtErrorResult(StatusCodes.Status404NotFound);
                case CmtErrorCodes.LastModified:
                    Log(LogLevel.Debug, ex);
                    return new CmtErrorResult(
                        StatusCodes.Status412PreconditionFailed,
                        CmtErrorCodes.LastModified);
                default:
                    return HandleException(ex);
            }
        }
    }
}
