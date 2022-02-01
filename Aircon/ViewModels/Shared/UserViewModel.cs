using Aircon.Business.Models.Shared;
using Aircon.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.ViewModels.Shared
{
    public class UserViewModel 
    {
        public int Id { get; set; }
        [Display(Name = "User ID")]
        public string DisplayUserId { get; set; } // Req
        [Required]
        [Display(Name = "First Name")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Please Enter a Valid Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Please Enter a Valid Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [Remote(action: "IsValidEmailCheck", controller: "User",areaName:"Customer",ErrorMessage ="This is not a business email", AdditionalFields = "Id,CustomerId")]
        [RegularExpression(@"^([a-zA-Z0-9_+\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please enter Valid Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Role", Prompt = "Role")]
        public string WorkTitle { get; set; }
        [Display(Name = "Phone", Prompt = "Phone")]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}(?: *x(\d{4,5}$))?$", ErrorMessage ="Enter a Valid PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Privileges", Prompt = "Privileges")]
        public string Role { get; set; }
        [Display(Name = "Status")]
        public UserStatus UserStatus { get; set; } // AwaitingReview
        public DateTime? CreationDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ActivatedDate { get; set; }
        public DateTime? SignedUpDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsEmployee { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public int? CustomerId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? CreationDateUtc { get; set; }
        public DateTime? ApprovedDateUtc { get; set; }
        public DateTime? ActivatedDateUtc { get; set; }
        public DateTime? SignedUpDateUtc { get; set; }

        public string FullName()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }

    }
}
