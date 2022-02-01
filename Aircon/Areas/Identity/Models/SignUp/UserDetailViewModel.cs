using Aircon.Business.Media;
using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Identity.Models.SignUp
{
    public class UserDetailViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [RegularExpression("^[a-zA-Z\\s]+$" ,ErrorMessage ="Please Enter a Valid Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please Enter a Valid Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Role")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Please Enter a Valid Role")]
        public string Role { get; set; }
        [Required]
        [Display(Name = "Phone")]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}(?: *x(\d{4,5}$))?$", ErrorMessage = "Enter a Valid PhoneNumber")]
        public string Phone { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }
        [Display(Name = "Branch Id")]
        public string BranchId { get; set; }
        public int UserId { get; set; }
        [UIHint("Picture")]
        public PictureModel Avatar { get; set; }
    }
}
