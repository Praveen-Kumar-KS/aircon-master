using Aircon.Data.Enums;

namespace Aircon.Business.Models.Shared
{
    public class PaymentMethodModel
    {
        public int CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentMethodDefault PaymentMethodDefault { get; set; }
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
        public AddressModel BillingAddress { get; set; }
    }
}
