using System;
using Cmt.WebApi.Infrastructure.HttpErrors;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public interface IExceptionHandler<in T> where T : Exception
    {
        HttpError Handle(T ex);
        HttpError Handle(int httpStatusCode);
    }
}
