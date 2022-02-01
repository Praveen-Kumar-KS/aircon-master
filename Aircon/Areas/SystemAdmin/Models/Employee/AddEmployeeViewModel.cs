using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.SystemAdmin.Models.Employee
{
    public class AddEmployeeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please enter Valid Email")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        public string Company { get; set; }
        public UserStatus UserStatus { get; set; } // AwaitingReview
        public String Privilages { get; set; }
        [Display(Name = "Role", Prompt = "Privileges")]
        public string Role { get; set; }
        public string DisplayUserId { get; set; } // Req
        public DateTime? ApprovedDate { get; set; }


    }
}
