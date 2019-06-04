using System.Collections.Generic;
using System.Linq;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.WebApi.ActionResults.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class AuthExceptionHandler : 
        ExceptionHandler,
        IExceptionHandler<AuthException>
    {
        public AuthExceptionHandler(ILogger<AuthExceptionHandler> logger)
            : base(logger) 
        { }

        public CmtErrorResult Handle(AuthException ex)
        { 
            var errors = ex.Errors.Select(x => x.Code).ToList();

            if (IsAllErrorsMatch(errors, GetNotFoundErrors(), 
                StatusCodes.Status404NotFound, out var notFound))
            {
                Log(LogLevel.Debug, ex);
                return notFound;
            }

            if (IsAllErrorsMatch(errors, GetForbiddenErrors(),
                StatusCodes.Status404NotFound, out var forbidden))
            {
                Log(LogLevel.Debug, ex);
                return forbidden;
            }

            return base.Handle(ex);
        }

        private bool IsAllErrorsMatch(
            IList<string> errors, 
            IList<string> matchWith,
            int statusCode,
            out CmtErrorResult errorResult)
        {
            if (errors.All(x => matchWith.Contains(x)))
            {
                errorResult = new CmtErrorResult(statusCode, errors);
                return true;
            }

            errorResult = null;
            return false;
        }

        private IList<string> GetForbiddenErrors() =>
            new List<string>
            {
                AuthErrorCodes.NotAllowed,
                AuthErrorCodes.UserNotInRole
            };

        private IList<string> GetNotFoundErrors() =>
            new List<string>
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
    }
}
