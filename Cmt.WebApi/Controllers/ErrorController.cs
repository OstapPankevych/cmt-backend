using Cmt.WebApi.ActionResults.Infrastructure;
using Cmt.WebApi.Infrastructure.ExceptionHandlers;
using Microsoft.AspNetCore.Mvc;

namespace Cmt.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ErrorController : CmtController
    {
        private readonly IExceptionHandlerFactory _exceptionHandlerFactory;

        public ErrorController(IExceptionHandlerFactory exceptionHandlerFactory)
        {
            _exceptionHandlerFactory = exceptionHandlerFactory; 
        }

        [Route("{code}")]
        public IActionResult Error(int code)
        {
            var httpError = _exceptionHandlerFactory.Create(code);

            return new CmtErrorResult(httpError);
        }
    }
}
