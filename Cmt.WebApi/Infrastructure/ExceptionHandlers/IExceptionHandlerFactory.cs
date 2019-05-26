using System;
using Cmt.WebApi.Infrastructure.HttpErrors;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers
{
    public interface IExceptionHandlerFactory
    {
        HttpError Create(Exception ex);
        HttpError Create(int httpErrorCode);
    }
}
