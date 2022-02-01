using Aircon.Business.Models.Shared;
using Aircon.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Aircon.Business.Models.Identity.SignUp
{
    public class CustomerOpportunityModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string FranchiseParent { get; set; }
        public string AdminEmail { get; set; }
        public string AlternateEmail { get; set; }
        public string IATANumber { get; set; }
        public string EinOrSsn { get; set; }
        public int NoOfBranchesId { get; set; }
        public string NoOfBranchesName { get; set; }
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public bool IsTermsAccepted { get; set; }
        public bool IsPaymentProcessed { get; set; }
        public bool IsSetupCompleted { get; set; }
        public DateTime? SignedUpDateUtc { get; set; }
        public DateTime? ApprovedDateUtc { get; set; }
        public DateTime? AbandonedDateUtc { get; set; }
        public DateTime? CallbackScheduledDateUtc { get; set; }
        public InterestLevel InterestLevel { get; set; } 
        public CustomerOpportunityStatus Status { get; set; }
        public NoOfBranches NoOfBranches { get; set; }
        public List<SelectListItem> SubscriptionTypes { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<PaymentMethodModel> PaymentMethods { get; set; }
        public List<NoteModel> Notes { get; set; }
        public int? AddressId { get; set; }
        public AddressModel MainAddress { get; set; }


    }
}
