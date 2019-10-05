using System.Collections.Generic;
using System.Linq;
using Cmt.Bll.Services.Exceptions.Auth;
using Cmt.WebApi.ActionResults.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class AuthExceptionHandler : 
        ExceptionHandler,
        IExceptionHandler<AuthException>
    {
        public AuthExceptionHandler(
            ILogger<AuthExceptionHandler> logger,
            IHostingEnvironment env)
            : base(logger, env) 
        { }

        public CmtErrorResult Handle(AuthException ex)
        { 
            var errors = ex.Errors.Select(x => x.Code).ToList();

            if (IsErrorsMatch(errors, GetNotFoundErrors()))
            {
                Log(LogLevel.Debug, ex);
                return new CmtErrorResult(StatusCodes.Status404NotFound, errors);
            }

            if (IsErrorsMatch(errors, GetForbiddenErrors()))
            {
                Log(LogLevel.Debug, ex);
                return new CmtErrorResult(StatusCodes.Status403Forbidden, errors);
            }

            return base.Handle(ex);
        }

        private bool IsErrorsMatch(
            IEnumerable<string> errors,
            IEnumerable<string> matchWith)
        {
            return errors.All(matchWith.Contains);
        }

        private IEnumerable<string> GetForbiddenErrors() =>
            new List<string>
            {
                AuthErrorCodes.NotAllowed,
                AuthErrorCodes.UserNotInRole
            };

        private IEnumerable<string> GetNotFoundErrors() =>
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
