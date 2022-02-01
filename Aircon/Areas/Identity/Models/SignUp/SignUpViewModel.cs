using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Aircon.Areas.Identity.Models.SignUp
{
    public class SignUpViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsValidEmail", controller: "Account", areaName: "Identity", ErrorMessage = "This is not a business email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

    }
}
