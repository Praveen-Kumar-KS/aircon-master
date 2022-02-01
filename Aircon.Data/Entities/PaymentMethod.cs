using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class PaymentMethod : AuditableEntity
    {
        public PaymentType PaymentType { get; set; } // Req CreditCard /ACH
        public PaymentMethodDefault PaymentMethodDefault { get; set; } // Req Subscription , Shippment, Subscription & Shippment
        public string CardNumber { get; set; }
        public string CardValidThrough { get; set; }
        public string CardCvv { get; set; }
        public string NameOnCard { get; set; }
        public int? BillingAddressId { get; set; }
        public bool IsBillingAddressSameAsCompanyAddress { get; set; }
        public string CompanyName { get; set; }
        public string AccountNumber { get; set; }
        public string Routing { get; set; }
        public string NameOnAccount { get; set; }
        public AccountType AccountType { get; set; }
        [ForeignKey("BillingAddressId")]
        public Address BillingAddress { get; set; }
    }
}
