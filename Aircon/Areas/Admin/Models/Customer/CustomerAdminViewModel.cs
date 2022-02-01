using Aircon.Areas.Customer.Models;
using Aircon.Areas.Customer.Models.Company;
using Aircon.Areas.Customer.Models.Contact;
using Aircon.Areas.Customer.Models.User;
using Aircon.Business.Models.Identity.SignUp;
using Aircon.Business.Models.Shared;
using Aircon.Data.Enums;
using Aircon.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Admin.Models.Customer
{
    public class CustomerAdminViewModel
    {
        public int CustomerId { get; set; }
        public string DisplayCustomerId { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        public string CompanyName { get; set; }
        [Display(Name = "Franchise Parent")]
        public string FranchiseParent { get; set; }
        [Display(Name = "Admin Name")]
        public string AdminName { get; set; }
        [Display(Name = "Phone")]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}(?: *x(\d{4,5}$))?$", ErrorMessage = "Enter a Valid PhoneNumber")]
        public string AdminPhoneNumber { get; set; }
        [Display(Name = "Admin Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter a Valid Email")]
        public string AdminEmail { get; set; }
        [Display(Name = "Alternate Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter a Valid Email")]
        public string AlternateEmail { get; set; }
        [Display(Name = "IATA Number")]
        public string IATANumber { get; set; }
        [Display(Name = "EinSsn")]
        public string EinOrSsn { get; set; }
        [Display(Name = "No Of Branches")]
        public NoOfBranches NoOfBranches { get; set; }
        [Display(Name = "Is Terms Accepted")]
        public bool IsTermsAccepted { get; set; }
        [Display(Name = "Is Payment Processed")]
        public bool IsPaymentProcessed { get; set; }
        [Display(Name = "Is Setup Completed")]
        public bool IsSetupCompleted { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Is Subscription Expired")]
        public bool IsSubscriptionExpired { get; set; }
        [Display(Name = "Subscription Expiry Date")]
        public DateTime SubscriptionExpiryDate { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }
        [Display(Name = "Activate Date")]
        public DateTime? ActivatedDate { get; set; }
        [Display(Name = "Signed Up Date")]
        public DateTime? SignedUpDate { get; set; }
        [Display(Name = "Approved Date")]
        public DateTime? ApprovedDate { get; set; }
        public int? CustomerOpportunityId { get; set; }
        public int SubscriptionId { get; set; }
        public int? AddressId { get; set; }
        public AddressViewModel MainAddress { get; set; }
        [Display(Name = "Credit Limit")]
        public decimal Creditlimit { get; set; }
    }
}
