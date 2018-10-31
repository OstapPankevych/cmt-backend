using System;
using System.Collections.Generic;
using System.Linq;
using Cmt.Bll.Services.Exceptions.Auth;
using Microsoft.AspNetCore.Http;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers.Handlers
{
    public class AuthExceptionHandler : IExceptionHandler<AuthException>
    {
        public HttpException Handle(AuthException ex)
        {
            var badRequest = new List<string>
            {
                AuthErrorCode.DuplicateEmail,
                AuthErrorCode.DuplicateUserName,
                AuthErrorCode.InvalidEmail,
                AuthErrorCode.InvalidUserName,
                AuthErrorCode.PasswordMismatch,
                AuthErrorCode.LoginAlreadyAssociated,
                AuthErrorCode.PasswordRequiresDigit,
                AuthErrorCode.PasswordTooShort,
                AuthErrorCode.PasswordRequiresDigit,
                AuthErrorCode.PasswordRequiresLower,
                AuthErrorCode.PasswordRequiresUpper,
                AuthErrorCode.PasswordRequiresNonAlphanumeric,
                AuthErrorCode.WrongLoginOrPassword
            };

            if (ex.Errors.All(x => badRequest.Contains(x.Code)))
            {
                return new HttpException
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Errors = ex.Errors.Select(x => x.Code)
                };
            }

            var forbidden = new[]
            {
                AuthErrorCode.NotAllowed,
                AuthErrorCode.UserNotInRole
            };

            return ex.Errors.All(x => forbidden.Contains(x.Code))
                ? new HttpException
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Errors = ex.Errors.Select(x => x.Code)
                }
                : new HttpException
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
        }
    }
}
