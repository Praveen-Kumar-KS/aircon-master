//using Aircon.Data.Enums;
//using System;
//using System.ComponentModel.DataAnnotations;

//namespace Aircon.Areas.SystemAdmin.Models.Employee
//{
//    // 4 User screen
//    public class EmployeeUserViewModel
//    {

//        public int Id { get; set; }
//        [Display(Name = "User Name")]
//        public string UserName { get; set; }
//        [Display(Name = "First Name")]
//        public string FirstName { get; set; }
//        [Display(Name = "Last Name")]
//        public string LastName { get; set; }
//        [Required]
//        [Display(Name = "Email")]
//        [RegularExpression(@"^([a-zA-Z0-9_+\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please enter Valid Email")]
//        public string Email { get; set; }
//        [Required]
//        [Display(Name = "Role", Prompt = "Privileges")]
//        public string Role { get; set; }

//        public string WorkTitle { get; set; }
//        [Display(Name = "Phone", Prompt = "Phone")]
//        public string PhoneNumber { get; set; }
//        public bool IsActive { get; set; }
//        [Display(Name = "User ID")]
//        public string DisplayUserId { get; set; } // Req
//        public DateTime? CreationDate { get; set; }
//        public DateTime? ApprovedDate { get; set; }
//        public DateTime? ActivatedDate { get; set; }
//        public DateTime? SignedUpDate { get; set; }
//        public bool IsApproved { get; set; }
//        public bool IsEmployee { get; set; }
//        [Display(Name = "Status")]
//        public UserStatus UserStatus { get; set; } // AwaitingReview

//        public string FullName()
//        {
//            return string.Format("{0} {1}", FirstName, LastName);
//        }
//    }
//}
