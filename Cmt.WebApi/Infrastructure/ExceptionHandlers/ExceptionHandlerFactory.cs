using System;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.WebApi.ActionResults.Infrastructure;
using Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers
{
    public class ExceptionHandlerFactory : IExceptionHandlerFactory
    {
        private readonly IExceptionHandler<Exception> _exceptionHandler;
        private readonly IExceptionHandler<AuthException> _authExceptionHandler;

        public ExceptionHandlerFactory(
            IExceptionHandler<Exception> exceptionHandler,
            IExceptionHandler<AuthException> authExceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
            _authExceptionHandler = authExceptionHandler;
        }

        public CmtErrorResult Create(Exception ex)
        {
            switch (ex)
            {
                case AuthException authException:
                    return _authExceptionHandler.Handle(authException);
                default:
                    return _exceptionHandler.Handle(ex);
            }
        }
    }
}
