using Aircon.Business.Media;
using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Customer.Models.Profile
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please enter Valid Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Role", Prompt = "Role")]
        public string WorkTitle { get; set; }
        [Display(Name = "Phone", Prompt = "Phone")]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$(?: *x(\d{4,5}$))?$", ErrorMessage = "Enter a Valid PhoneNumber")]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        [UIHint("Picture")]
        public PictureModel Avatar { get; set; }
    }
}
