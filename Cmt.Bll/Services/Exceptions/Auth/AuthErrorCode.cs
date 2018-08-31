using System;
namespace Cmt.Bll.Services.Exceptions.Auth
{
    public static class AuthErrorCode
    {
        public static string LockedOut = "lockedOut";
        public static string NotAllowed = "notAllowed";
        public static string RequiresTwoFactor = "requiresTwoFactor";
        public static string WrongLoginOrPassword = "wrongLoginOrPassword";
        public static string DefaultError = "defaultError";
        public static string ConcurrencyFailure = "concurrencyFailure";
        public static string PasswordMismatch = "passwordMismatch";
        public static string InvalidToken = "invalidToken";
        public static string LoginAlreadyAssociated = "loginAlreadyAssociated";
        public static string InvalidUserName = "invalidUserName";
        public static string DuplicateEmail = "duplicateEmail";
        public static string InvalidEmail = "invalidEmail";
        public static string DuplicateUserName = "duplicateUserName";
        public static string InvalidRoleName = "invalidRoleName";
        public static string DuplicateRoleName = "duplicateRoleName";
        public static string UserAlreadyHasPassword = "userAlreadyHasPassword";
        public static string UserLockoutNotEnabled = "userLockoutNotEnabled";
        public static string UserAlreadyInRole = "userAlreadyInRole";
        public static string UserNotInRole = "userNotInRole";
        public static string PasswordTooShort = "passwordTooShort";
        public static string PasswordRequiresNonAlphanumeric = "passwordRequiresNonAlphanumeric";
        public static string PasswordRequiresDigit = "passwordRequiresDigit";
        public static string PasswordRequiresLower = "passwordRequiresLower";
        public static string PasswordRequiresUpper = "passwordRequiresUpper";
    }
}
