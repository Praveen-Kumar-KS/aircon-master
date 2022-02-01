//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Aircon.Areas.Admin.Models.Customer
//{
//    public class EditCustomerProfileViewModel
//    {
//        public int CustomerId { get; set; }
//        public string DisplayCustomerId { get; set; }
//        [Display(Name = "Company Name")]
//        [Required]
//        public string CompanyName { get; set; }

//        [Display(Name = "Admin Name")]
//        public string AdminName { get; set; }
//        [Display(Name = "Phone Number")]
//        public string AdminPhoneNumber { get; set; }
//        [Display(Name = "Email")]
//        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
//        ErrorMessage = "Please enter a Valid Email")]
//        public string AdminEmail { get; set; }
//        [Display(Name = "EinSsn")]
//        public string EinOrSsn { get; set; }
//        public bool IsActive { get; set; }
//        [Display(Name = "Credit Limit")]
//        public Decimal Creditlimit { get; set; }
//        [Display(Name = "Creation Date")]
//        public DateTime? CreationDate { get; set; }
//        [Display(Name = "Activate Date")]
//        public DateTime? ActivatedDate { get; set; }
//        [Display(Name = "Signed Up Date")]
//        public DateTime? SignedUpDate { get; set; }
//        [Display(Name = "Approved Date")]
//        public DateTime? ApprovedDate { get; set; }
//        public int? CustomerOpportunityId { get; set; }
      
//    }
//}
