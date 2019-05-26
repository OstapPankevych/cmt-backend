using Cmt.Bll.Services.Exceptions;
using Cmt.WebApi.Infrastructure.HttpErrors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class CmtExceptionHandler:
        ExceptionHandler,
        IExceptionHandler<CmtException>
    {
        public CmtExceptionHandler(ILogger<CmtExceptionHandler> logger)
            : base(logger)
        { }

        public HttpError Handle(CmtException ex)
        {
            var code = ex.Error.Code;
            HttpError httpError;
            switch (code)
            {
                case CmtErrorCodes.NotFound:
                    httpError = CreateHttpError(StatusCodes.Status404NotFound);
                    Log(LogLevel.Debug, ex, httpError);
                    break;
                case CmtErrorCodes.LastModified:
                    httpError = CreateHttpError(StatusCodes.Status412PreconditionFailed, code);
                    Log(LogLevel.Debug, ex, httpError);
                    break;
                default:
                    return base.Handle(ex);
            }

            return httpError;
        }
    }
}
