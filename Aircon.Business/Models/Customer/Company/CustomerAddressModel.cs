using Aircon.Business.Models.Shared;

namespace Aircon.Business.Models.Customer.Company
{
    public class CustomerAddressModel
    {
        public int CustomerId { get; set; }
        public int AddressTypeId { get; set; } // Req
        public int AddressId { get; set; }
        public string AddressTypeName { get; set; }
        public AddressModel Address { get; set; }
    }
}
