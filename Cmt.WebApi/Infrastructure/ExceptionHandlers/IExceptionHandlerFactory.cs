using System;
namespace Cmt.WebApi.Infrastructure.ExceptionHandlers
{
    public interface IExceptionHandlerFactory
    {
        HttpError Create(Exception ex);
    }
}
