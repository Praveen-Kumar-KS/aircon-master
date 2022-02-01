using Aircon.Business.Models.Customer.Company;
using Aircon.Business.Models.Customer.Contact;
using Aircon.Business.Models.Shared;
using Aircon.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Aircon.Business.Models.Admin.Customer
{
    public class CustomerAdminModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string DisplayCustomerId { get; set; }
        public string CompanyName { get; set; }
        public string FranchiseParent { get; set; }
        public string AdminEmail { get; set; }
        public string AdminName { get; set; }
        public string AdminPhoneNumber { get; set; }
        public string AlternateEmail { get; set; }
        public string IATANumber { get; set; }
        public string EinOrSsn { get; set; }
        public int SubscriptionId { get; set; }
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
        public AddressModel MainAddress { get; set; }
        public NoOfBranches NoOfBranches { get; set; }
        public decimal Creditlimit { get; set; }
    }
}
