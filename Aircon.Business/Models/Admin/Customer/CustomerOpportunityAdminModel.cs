using Aircon.Business.Models.Shared;
using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Admin.Customer
{
    public class CustomerOpportunityAdminModel
    {
        public int Id { get; set; }
        public int CustomerOpportunityId { get; set; }
        public string CompanyName { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AlternateEmail { get; set; }
        public string IATANumber { get; set; }
        public string AdminPhoneNumber { get; set; }
        public string FranchiseParent { get; set; }
        public string EinOrSsn { get; set; } // Req
        public int? SubscriptionId { get; set; }
        public bool IsTermsAccepted { get; set; }
        public bool IsPaymentProcessed { get; set; }
        public bool IsSetupCompleted { get; set; }
        public DateTime? SignedUpDateUtc { get; set; }
        public DateTime? ApprovedDateUtc { get; set; }
        public DateTime? AbandonedDateUtc { get; set; }
        public DateTime? CallbackScheduledDateUtc { get; set; }
        public InterestLevel InterestLevel { get; set; } // High, Low, Medium
        public CustomerOpportunityStatus Status { get; set; } // CallBackScheduled, Abadaonded, Approved.
        public int? AddressId { get; set; }
        public AddressModel MainAddress { get; set; }
        public NoOfBranches NoOfBranches { get; set; }
    }
}
