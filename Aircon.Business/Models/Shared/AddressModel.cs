using Aircon.Business.Media;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Aircon.Business.Models.Shared
{
    public class AddressModel
    {
        public string NickName { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
    }

    public class CustomerOpportunityAddressModel : AddressModel
    {
        public int CustomerOpportunityId { get; set; }
        public int AddressTypeId { get; set; }
        public PictureModel Logo { get; set; }
    }
}
