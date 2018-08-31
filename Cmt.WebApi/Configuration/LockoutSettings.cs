using System;
namespace Cmt.WebApi.Configuration
{
    public class LockoutSettings
    {
        public int DefaultLockoutMinutes { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
        public bool AllowedForNewUsers { get; set; }
    }
}
