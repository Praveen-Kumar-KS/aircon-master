using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class CustomerOpportunity : AuditableEntity
    {
        public CustomerOpportunity()
        {
            CustomerOpportunityAddresses = new List<CustomerOpportunityAddress>();
            PaymentMethods = new List<CustomerOpportunityPaymentMethod>();
            CustomerOpportunityNotes = new List<CustomerOpportunityNote>();
            Users = new List<User>();
        }
        public string CompanyName { get; set; } // Req
        public string FranchiseParent { get; set; } 
        public string AdminEmail { get; set; } // Req
        public string AdminName { get; set; } // Req
        public string AdminPhoneNumber { get; set; }
        public string AlternateEmail { get; set; }
        public string IATANumber { get; set; } // Req
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
        public int? LogoId { get; set; }
        [ForeignKey("LogoId")]
        public Attachment Logo { get; set; }

        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address MainAddress { get; set; }
        public NoOfBranches NoOfBranches { get; set; }
        [ForeignKey("SubscriptionId")]
        public SubscriptionType Subscription { get; set; }
        public ICollection<CustomerOpportunityAddress> CustomerOpportunityAddresses { get; set; }
        public ICollection<CustomerOpportunityPaymentMethod> PaymentMethods { get; set; }
        public ICollection<CustomerOpportunityNote> CustomerOpportunityNotes { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
