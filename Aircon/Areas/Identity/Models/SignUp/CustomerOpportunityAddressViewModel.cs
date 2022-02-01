using Aircon.Business.Media;
using Aircon.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Identity.Models.SignUp
{
    public class CustomerOpportunityAddressViewModel : AddressViewModel
    {
        public int CustomerOpportunityId { get; set; }
        public int AddressTypeId { get; set; }
        [UIHint("Picture")]
        public PictureModel Logo { get; set; }
    }
}
