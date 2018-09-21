using System;
using System.ComponentModel.DataAnnotations;

namespace Cmt.WebApi.Models.Users
{
    public class NewUser
    {
        [Required]
        [EmailAddress]
        [MaxLength(ValidationConstants.Length256)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(ValidationConstants.AplhabetDigitLetterReg)]
        [MaxLength(ValidationConstants.Length256)]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
