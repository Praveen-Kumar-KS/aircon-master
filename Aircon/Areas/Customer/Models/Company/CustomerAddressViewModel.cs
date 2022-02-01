using Aircon.ViewModels.Shared;

namespace Aircon.Areas.Customer.Models.Company
{
    public class CustomerAddressViewModel
    {
        public int CustomerId { get; set; }
        public int AddressTypeId { get; set; } // Req
        public int AddressId { get; set; }
        public string AddressTypeName { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
