using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Aircon.Areas.Identity.Models.SignUp
{
    public class SignUpCompanyProfileViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "Please Enter a Valid Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Franchise Parent")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "Please Enter a Valid Name")]
        public string FranchiseParent { get; set; }
        [Display(Name = "Admin Email")]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter a Valid Email")]
        public string AdminEmail { get; set; }
        [Display(Name = "Alternate Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter a Valid Email")]
        public string AlternateEmail { get; set; }
        [Display(Name = "IATA Number")]
        [Required]
        public string IATANumber { get; set; }
        [Display(Name = "Ein Or Ssn")]
        [Required]
        public string EinOrSsn { get; set; }

    }
}
