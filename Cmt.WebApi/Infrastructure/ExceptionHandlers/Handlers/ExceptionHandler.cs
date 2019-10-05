using System;
using Cmt.Bll.Services.Exceptions;
using Cmt.WebApi.ActionResults.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class ExceptionHandler:
        IExceptionHandler<Exception>
    {
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly IHostingEnvironment _env;

        public ExceptionHandler(
            ILogger<ExceptionHandler> logger,
            IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public CmtErrorResult Handle(Exception ex)
        {
            return ex is CmtException cmtException
                ? HandleCmtException(cmtException)
                : HandleException(ex);
        }

        protected void Log(LogLevel logLevel, Exception ex)
        {
          _logger.Log(logLevel, ex.Message);
        }

        private CmtErrorResult HandleException(Exception ex)
        {
            Log(LogLevel.Error, ex);

            var err = _env.IsDevelopment()
                ? (ex.GetType().ToString(), $"inner: {ex.Message};{ex.InnerException}")
                : (CmtErrorCodes.Unknown, null);

            return new CmtErrorResult(StatusCodes.Status500InternalServerError, err.Item1, err.Item2);
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
