using System;
namespace Cmt.WebApi.ExceptionHandlers.Handlers
{
    public interface IExceptionHandler<T> where T : Exception
    {
        HttpException Handle(T ex);
    }
}
