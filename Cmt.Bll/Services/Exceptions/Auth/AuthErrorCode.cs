namespace Cmt.Bll.Services.Exceptions.Auth
{
    public static class AuthErrorCodes
    {
        public static string LockedOut = "LockedOut";
        public static string NotAllowed = "NotAllowed";
        public static string RequiresTwoFactor = "RequiresTwoFactor";
        public static string WrongLoginOrPassword = "WrongLoginOrPassword";
        public static string DefaultError = "DefaultError";
        public static string ConcurrencyFailure = "ConcurrencyFailure";
        public static string PasswordMismatch = "PasswordMismatch";
        public static string InvalidToken = "InvalidToken";
        public static string LoginAlreadyAssociated = "LoginAlreadyAssociated";
        public static string InvalidUserName = "InvalidUserName";
        public static string DuplicateEmail = "DuplicateEmail";
        public static string InvalidEmail = "InvalidEmail";
        public static string DuplicateUserName = "DuplicateUserName";
        public static string InvalidRoleName = "InvalidRoleName";
        public static string DuplicateRoleName = "DuplicateRoleName";
        public static string UserAlreadyHasPassword = "UserAlreadyHasPassword";
        public static string UserLockoutNotEnabled = "UserLockoutNotEnabled";
        public static string UserAlreadyInRole = "UserAlreadyInRole";
        public static string UserNotInRole = "UserNotInRole";
        public static string PasswordTooShort = "PasswordTooShort";
        public static string PasswordRequiresNonAlphanumeric = "PasswordRequiresNonAlphanumeric";
        public static string PasswordRequiresDigit = "PasswordRequiresDigit";
        public static string PasswordRequiresLower = "PasswordRequiresLower";
        public static string PasswordRequiresUpper = "PasswordRequiresUpper";
    }
}
