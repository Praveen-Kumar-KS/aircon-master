using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Identity.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Password")]
        public bool RememberMe { get; set; }
    }


}
