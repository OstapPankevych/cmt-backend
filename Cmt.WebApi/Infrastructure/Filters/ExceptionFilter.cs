using Cmt.WebApi.ActionResults.Infrastructure;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cmt.WebApi.Infrastructure.Filters
{
    public class ExceptionFilter: IExceptionFilter
    {
        private readonly IExceptionHandlerFactory _exceptionHandlerFactory;

        public ExceptionFilter(IExceptionHandlerFactory exceptionHandlerFactory)
        {
            _exceptionHandlerFactory = exceptionHandlerFactory;
        }

        public void OnException(ExceptionContext context)
        {
            var httpError = _exceptionHandlerFactory.Create(context.Exception);
            context.Result = new CmtErrorResult(httpError);
        }
    }
}
