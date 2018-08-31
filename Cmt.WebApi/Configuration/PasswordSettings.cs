using System;
namespace Cmt.WebApi.Configuration
{
    public class PasswordSettigs
    {
        public bool RequireDigit { get; set; }
        public int RequiredLength { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireLowercase { get; set; }
        public int RequiredUniqueChars { get; set; }
        public string Salt { get; set; }
    }
}
