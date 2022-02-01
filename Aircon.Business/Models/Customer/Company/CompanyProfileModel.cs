using Aircon.Business.Media;
using Aircon.Business.Models.Shared;

namespace Aircon.Business.Models.Customer.Company
{
    public class CompanyProfileModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int AddressId { get; set; }
        public AddressModel MainAddress { get; set; }
        public PictureModel Logo { get; set; }
    }
}
