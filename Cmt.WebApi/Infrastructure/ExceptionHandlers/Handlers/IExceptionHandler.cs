using System;
using Cmt.WebApi.ActionResults.Infrastructure;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public interface IExceptionHandler<in T> where T : Exception
    {
        CmtErrorResult Handle(T ex);
    }
}
