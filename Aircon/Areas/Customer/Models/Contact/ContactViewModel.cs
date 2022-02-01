using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Customer.Models.Contact
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please Enter a Valid Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please Enter a Valid Name")]
        public string LastName { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "Please Enter a Valid Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Department")]
        public string Department { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}(?: *x(\d{4,5}$))?$", ErrorMessage = "Enter a Valid PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter a Valid Email")]
        public string Email { get; set; }
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [Display(Name = "Special Instruction")]
        public string SpecialInstruction { get; set; }
        public string NickName { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

    }
}
