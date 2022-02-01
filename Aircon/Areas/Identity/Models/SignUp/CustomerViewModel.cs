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

namespace Aircon.Areas.Identity.Models.SignUp
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string DisplayCustomerId { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        public string CompanyName { get; set; }
        [Display(Name = "Franchise Parent")]
        public string FranchiseParent { get; set; }
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
        public string EinSsn { get; set; }
        [Display(Name = "No Of Branches")]
        public int NoOfBranchesId { get; set; }
        public string NoOfBranchesName { get; set; }
        [Display(Name = "Subscription Name")]
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
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
        public int? AddressId { get; set; }
        public NoOfBranches NoOfBranches { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<SelectListItem> NoOfBranchesTypes { get; set; }
        public List<SelectListItem> SubscriptionTypes { get; set; }
        public List<PaymentMethodViewModel> PaymentMethods { get; set; }
        public List<ContactViewModel> Contacts { get; set; }
        public List<UserViewModel> Users { get; set; }
        public List<NoteViewModel> Notes { get; set; }

    }
}
