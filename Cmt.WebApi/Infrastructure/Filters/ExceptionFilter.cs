using System.Threading.Tasks;
using Cmt.WebApi.Infrastructure.ActionResults;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cmt.WebApi.Infrastructure.Filters
{
    public class ExceptionFilter: IAsyncExceptionFilter
    {   
        private readonly IExceptionHandlerFactory _exceptionHandlerFactory;

        public ExceptionFilter(IExceptionHandlerFactory exceptionHandlerFactory)
        {
            _exceptionHandlerFactory = exceptionHandlerFactory;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var httpError = _exceptionHandlerFactory.Create(context.Exception);
            context.Result = new ErrorResult(httpError);
            context.ExceptionHandled = true;

            await Task.CompletedTask;
        }
    }
}
