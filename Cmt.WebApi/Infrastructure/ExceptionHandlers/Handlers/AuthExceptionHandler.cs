using System.Collections.Generic;
using System.Linq;
using Cmt.Bll.Services.Exceptions.Auth;
using Microsoft.AspNetCore.Http;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class AuthExceptionHandler :
        ExceptionHandler, IExceptionHandler<AuthException>
    {
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

            var errors = ex.Errors.Select(x => x.Code).ToList();

            if (IsErrorsAllFrom(errors, badRequest))
            {
                return new HttpError
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Errors = errors
                };
            }

            var forbidden = new List<string>
            {
                AuthErrorCodes.NotAllowed,
                AuthErrorCodes.UserNotInRole
            };

            if (IsErrorsAllFrom(errors, forbidden))
            {
                return new HttpError
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Errors = errors
                };
            }

            return base.Handle(ex);
        }

        private bool IsErrorsAllFrom(List<string> errros, List<string> from)
        {
            return errros.All(x => from.Contains(x));
        }
    }
}
