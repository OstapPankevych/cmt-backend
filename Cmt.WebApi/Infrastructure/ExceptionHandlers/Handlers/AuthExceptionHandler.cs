using System.Collections.Generic;
using System.Linq;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.WebApi.Infrastructure.HttpErrors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class AuthExceptionHandler :
        ExceptionHandler, IExceptionHandler<AuthException>
    {
        public AuthExceptionHandler(ILogger<AuthExceptionHandler> logger)
            : base(logger) 
        { }

        public HttpError Handle(AuthException ex)
        {
            var badRequest = new List<string>
            {
                AuthErrorCodes.DuplicateEmail,
                AuthErrorCodes.DuplicateUserName,
                AuthErrorCodes.InvalidEmail,
                AuthErrorCodes.InvalidUserName,
                AuthErrorCodes.PasswordMismatch,
                AuthErrorCodes.LoginAlreadyAssociated,
                AuthErrorCodes.PasswordRequiresDigit,
                AuthErrorCodes.PasswordTooShort,
                AuthErrorCodes.PasswordRequiresDigit,
                AuthErrorCodes.PasswordRequiresLower,
                AuthErrorCodes.PasswordRequiresUpper,
                AuthErrorCodes.PasswordRequiresNonAlphanumeric,
                AuthErrorCodes.WrongLoginOrPassword
            };

            var forbidden = new List<string>
            {
                AuthErrorCodes.NotAllowed,
                AuthErrorCodes.UserNotInRole
            };

            var errors = ex.Errors.Select(x => x.Code).ToList();
            HttpError httpError = null;

            if (IsErrorsAllFrom(errors, badRequest))
            {
                httpError = CreateHttpError(StatusCodes.Status400BadRequest, errors);
            }
            else if (IsErrorsAllFrom(errors, forbidden))
            {
                httpError = CreateHttpError(StatusCodes.Status403Forbidden, errors);
            }

            if (httpError != null)
            {
                Log(LogLevel.Debug, ex, httpError);
                return httpError;
            }

            return base.Handle(ex);
        }

        private bool IsErrorsAllFrom(List<string> errros, List<string> from)
        {
            return errros.All(x => from.Contains(x));
        }
    }
}
