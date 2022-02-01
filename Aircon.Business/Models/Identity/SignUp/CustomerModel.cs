using Aircon.Business.Models.Customer.Contact;
using Aircon.Business.Models.Shared;
using Aircon.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Aircon.Business.Models.Identity.SignUp
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string DisplayCustomerId { get; set; }
        public string CompanyName { get; set; }
        public string FranchiseParent { get; set; }
        public string AdminEmail { get; set; }
        public string AlternateEmail { get; set; }
        public string IATANumber { get; set; }
        public string EinSsn { get; set; }
        public int NoOfBranchesId { get; set; }
        public string NoOfBranchesName { get; set; }
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public bool IsTermsAccepted { get; set; }
        public bool IsPaymentProcessed { get; set; }
        public bool IsSetupCompleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsSubscriptionExpired { get; set; }
        public DateTime SubscriptionExpiryDateUtc { get; set; }
        public DateTime? CreationDateUtc { get; set; }
        public DateTime? ActivatedDateUtc { get; set; }
        public DateTime? SignedUpDateUtc { get; set; }
        public DateTime? ApprovedDateUtc { get; set; }
        public int? CustomerOpportunityId { get; set; }
        public int? AddressId { get; set; }
        public NoOfBranches NoOfBranches { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<SelectListItem> NoOfBranchesTypes { get; set; }
        public List<SelectListItem> SubscriptionTypes { get; set; }
        public List<PaymentMethodModel> PaymentMethods { get; set; }
        public List<ContactModel> Contacts { get; set; }
        public List<UserModel> Users { get; set; }
        public List<NoteModel> Notes { get; set; }



    }
}
