using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class Customer : AuditableEntity
    {
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

        public decimal Creditlimit { get; set; }
        public DateTime? CreationDateUtc { get; set; }
        public DateTime? ActivatedDateUtc { get; set; }
        public DateTime? SignedUpDateUtc { get; set; }
        public DateTime? ApprovedDateUtc { get; set; }


        public int? CustomerOpportunityId { get; set; }
        [ForeignKey("CustomerOpportunityId")]
        public CustomerOpportunity CustomerOpportunity { get; set; }

        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address MainAddress { get; set; }

        public NoOfBranches NoOfBranches { get; set; }
        [ForeignKey("SubscriptionId")]

        public int? LogoId { get; set; }
        [ForeignKey("LogoId")]
        public Attachment Logo { get; set; }
        public SubscriptionType Subscription { get; set; }
        public ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public ICollection<CustomerPaymentMethod> PaymentMethods { get; set; }
        public ICollection<CustomerContact> CustomerContacts { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<CustomerNote> CustomerNotes { get; set; }
        public ICollection<CustomerDomain> CustomerDomains { get;set;}
        public Customer()
        {
            CustomerAddresses = new List<CustomerAddress>();
            PaymentMethods = new List<CustomerPaymentMethod>();
            CustomerContacts = new List<CustomerContact>();
            Users = new List<User>();
            CustomerNotes = new List<CustomerNote>();
            CustomerDomains = new List<CustomerDomain>();
        }
    }
}
