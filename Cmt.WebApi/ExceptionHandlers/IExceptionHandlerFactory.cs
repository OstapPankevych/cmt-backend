using System;
namespace Cmt.WebApi.ExceptionHandlers
{
    public interface IExceptionHandlerFactory
    {
        HttpException Create(Exception ex);
    }
}
