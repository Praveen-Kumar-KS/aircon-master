//using Aircon.Business.Models.Shared;
//using Aircon.Data.Enums;
//using System;
//using System.ComponentModel.DataAnnotations;

//namespace Aircon.Areas.Customer.Models.User
//{
//    // 4 User screen
//    public class CustomerUserViewModel 
//    {

//        public int Id { get; set; }
//        [Display(Name = "User ID")]
//        public string DisplayUserId { get; set; } // Req
//        [Required]
//        [Display(Name = "First Name")]
//        public string FirstName { get; set; }
//        [Display(Name = "Last Name")]
//        public string LastName { get; set; }
//        [Required]
//        [Display(Name = "Email")]
//        [RegularExpression(@"^([a-zA-Z0-9_+\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please enter Valid Email")]
//        public string Email { get; set; }
//        [Required]
//        [Display(Name = "Role", Prompt = "Role")]
//        public string WorkTitle { get; set; }
//        [Display(Name = "Phone", Prompt = "Phone")]
//        public string PhoneNumber { get; set; }
//        [Display(Name = "Privileges", Prompt = "Privileges")]
//        public string Role { get; set; }
//        [Display(Name = "Status")]
//        public UserStatus UserStatus { get; set; } // AwaitingReview
//        public DateTime? CreationDate { get; set; }
//        public DateTime? ApprovedDate { get; set; }
//        public DateTime? ActivatedDate { get; set; }
//        public DateTime? SignedUpDate { get; set; }
//        public bool IsApproved { get; set; }
//        public bool IsEmployee { get; set; }
//        public string UserName { get; set; }
//        public bool IsActive { get; set; }
//        public int CustomerId { get; set; }
//        public string CompanyName { get; set; }
//        public DateTime? CreationDateUtc { get; set; }
//        public DateTime? ApprovedDateUtc { get; set; }
//        public DateTime? ActivatedDateUtc { get; set; }
//        public DateTime? SignedUpDateUtc { get; set; }

//        public string FullName()
//        {
//            return string.Format("{0} {1}", FirstName, LastName);
//        }

//    }
//}
