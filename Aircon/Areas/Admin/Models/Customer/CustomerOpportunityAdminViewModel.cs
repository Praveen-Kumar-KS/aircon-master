using Aircon.Business.Models.Shared;
using Aircon.Data.Enums;
using Aircon.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Admin.Models.Customer
{
    public class CustomerOpportunityAdminViewModel
    {
        public int CustomerOpportunityId { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        public string CompanyName { get; set; }
        [Display(Name = "Franchise Parent")]
        public string FranchiseParent { get; set; }
        [Display(Name = "Admin Name")]
        public string AdminName { get; set; }
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
        [Display(Name = "EinOrSsn")]
        [Required]
        public string EinOrSsn { get; set; }
        [Display(Name = "Phone Number")]
        public string AdminPhoneNumber { get; set; }
        [Display(Name = "Subscriptions")]
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        [Display(Name = "Is Terms Accepted")]
        public bool IsTermsAccepted { get; set; }
        [Display(Name = "Is Payment Processed")]
        public bool IsPaymentProcessed { get; set; }
        [Display(Name = "Is Setup Completed")]
        public bool IsSetupCompleted { get; set; }
        [Display(Name = "Signed Up Date")]
        public DateTime? SignedUpDate { get; set; }
        [Display(Name = "Approved Date")]
        public DateTime? ApprovedDate { get; set; }
        [Display(Name = "Abandoned Date")]
        public DateTime? AbandonedDate { get; set; }
        [Display(Name = "Callback Scheduled Date")]
        public DateTime? CallbackScheduledDate { get; set; }
        public InterestLevel InterestLevel { get; set; }
        public CustomerOpportunityStatus Status { get; set; }
        public NoOfBranches NoOfBranches { get; set; }
        public int? AddressId { get; set; }
        public AddressViewModel MainAddress { get; set; }
    }
}
