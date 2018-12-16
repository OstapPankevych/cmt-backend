using System;
namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public interface IExceptionHandler<T> where T : Exception
    {
        HttpError Handle(T ex);
    }
}
