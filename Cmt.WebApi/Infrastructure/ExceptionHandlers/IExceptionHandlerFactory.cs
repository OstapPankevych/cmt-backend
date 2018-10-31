using System;
namespace Cmt.WebApi.Infrastructure.ExceptionHandlers
{
    public interface IExceptionHandlerFactory
    {
        HttpException Create(Exception ex);
    }
}
