using System;
using Cmt.Bll.Services.Exceptions;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers;
using Cmt.WebApi.Infrastructure.HttpErrors;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers
{
    public class ExceptionHandlerFactory : IExceptionHandlerFactory
    {
        private readonly IExceptionHandler<Exception> _exceptionHandler;
        private readonly IExceptionHandler<CmtException> _cmtExceptionHandler;
        private readonly IExceptionHandler<AuthException> _authExceptionHandler;

        public ExceptionHandlerFactory(
            IExceptionHandler<Exception> exceptionHandler,
            IExceptionHandler<CmtException> cmtExceptionHandler,
            IExceptionHandler<AuthException> authExceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
            _cmtExceptionHandler = cmtExceptionHandler;
            _authExceptionHandler = authExceptionHandler;
        }

        public HttpError Create(Exception ex)
        {
            switch (ex)
            {
                case AuthException authException:
                    return _authExceptionHandler.Handle(authException);
                case CmtException cmtException:
                    return _cmtExceptionHandler.Handle(cmtException);
                default:
                    return _exceptionHandler.Handle(ex);
            }
        }

        public HttpError Create(int httpStatusCode)
        {
            return _exceptionHandler.Handle(httpStatusCode);
        }
    }
}
