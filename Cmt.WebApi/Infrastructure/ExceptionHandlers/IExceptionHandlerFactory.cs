using System;
using Cmt.WebApi.ActionResults.Infrastructure;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers
{
    public interface IExceptionHandlerFactory
    {
        CmtErrorResult Create(Exception ex);
    }
}
