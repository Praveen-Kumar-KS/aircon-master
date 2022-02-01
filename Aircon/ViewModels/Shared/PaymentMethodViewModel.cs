using Aircon.Areas.Customer.Models;
using Aircon.Data.Enums;
using Aircon.ViewModels.Shared;
using System.ComponentModel.DataAnnotations;


namespace Aircon.ViewModels.Shared
{
    public class PaymentMethodViewModel : ValidationAttribute
    {
        public int CustomerId { get; set; }
        [Required]
        public int PaymentMethodId { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        [Required]
        public PaymentMethodDefault PaymentMethodDefault { get; set; }
        [Display(Name = "Card Number")]
        [CreditCard]
        [Required]
        public string CardNumber { get; set; }
        [Display(Name = "Card Valid Through")]
        [RegularExpression(@"^(0\d|1[0-2])\/\d{2}$",
        ErrorMessage = "Please enter a Valid Date")]
        [Required]
        public string CardValidThrough { get; set; }
        [Display(Name = "Card Cvv")]
        [RegularExpression(@"^[0-9]{3,3}$",
        ErrorMessage = "Please enter a Valid Cvv")]
        [Required]
        public string CardCvv { get; set; }
        [Display(Name = "Name On Card")]
        [RegularExpression(@"^[a-zA-Z]+([\s][a-zA-Z]+)*",
        ErrorMessage = "Please enter a Valid Name")]
        [Required]
        public string NameOnCard { get; set; }
        public int? BillingAddressId { get; set; }
        [Display(Name = "Same as company address")]
        public bool IsBillingAddressSameAsCompanyAddress { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Account Number")]
        [Required]
        [RegularExpression(@"^[0-9]{9,18}$",
        ErrorMessage = "Please enter a Valid Account Number")]
        public string  AccountNumber { get; set; }
        [Display(Name = "Routing")]
        [RegularExpression(@"^[0-9]{7,9}$",
        ErrorMessage = "Please enter a Valid Routing Number")]
        [Required]
        public string Routing { get; set; }
        [Display(Name = "Re-enter Account Number")]
        [Required]
        public string ConfirmAccountNumber { get; set; }
        [Display(Name = "Re-enter Routing")]
        [Required]
        public string ConfirmRouting { get; set; }
        [Display(Name = "Name On Account")]
        [RegularExpression(@"^[a-zA-Z]+$",
        ErrorMessage = "Please enter a Valid Name")]
        public string NameOnAccount { get; set; }
        [Display(Name = "Account Type Name")]
        public AccountType AccountType { get; set; }
        public AddressViewModel BillingAddress { get; set; }
      
    }
}
