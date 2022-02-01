using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Identity.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }


}
