using System;
using System.ComponentModel.DataAnnotations;

namespace Cmt.WebApi.Models.Users
{
    public class SignInUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
