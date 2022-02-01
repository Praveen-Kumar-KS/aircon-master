using Aircon.Areas.Customer.Models.Company;
using Aircon.ViewModels.Shared;
using System.Collections.Generic;

namespace Aircon.Areas.Customer.Models.Company
{
    public class CompanyViewModel
    {
        public CompanyProfileViewModel CompanyProfile { get; set; }
        public List<PaymentMethodViewModel> PaymentMethods { get; set; }
        public List<CustomerAddressViewModel> CustomerAddresses { get; set; }
        public CompanyViewModel()
        {
            CompanyProfile = new CompanyProfileViewModel();
            PaymentMethods = new List<PaymentMethodViewModel>();
            CustomerAddresses = new List<CustomerAddressViewModel>();
        }
    }
}
