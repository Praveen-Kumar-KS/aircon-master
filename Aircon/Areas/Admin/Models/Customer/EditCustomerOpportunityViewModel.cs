//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Aircon.Areas.Admin.Models.Customer
//{
//    public class EditCustomerOpportunityViewModel
//    {
//        public int CustomerOpportunityId { get; set; }
//        [Display(Name = "Company Name")]
//        [Required]
//        public string CompanyName { get; set; }
//        [Display(Name = "Admin Name")]
//        public string AdminName { get; set; }
//        [Display(Name = "Email")]
//        [Required]
//        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
//        ErrorMessage = "Please enter a Valid Email")]
//        public string AdminEmail { get; set; }
//        [Display(Name = "Phone Number")]
//        public string AdminPhoneNumber { get; set; }
//        [Display(Name = "EinSsn")]
//        public string EinOrSsn { get; set; }
//        public DateTime? SignedUpDate { get; set; }
//        [Display(Name = "Approved Date")]
//        public DateTime? ApprovedDate { get; set; }
//        [Display(Name = "Abandoned Date")]
//        public DateTime? AbandonedDate { get; set; }
//        [Display(Name = "Callback Date")]
//        public DateTime? CallbackScheduledDate { get; set; }

//    }
//}
