using Models.Consts;
using System.ComponentModel.DataAnnotations;

namespace Models.Auth
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(ValidationConsts.MinPasswordLength)]
        public string Password { get; set; }
    }
}
