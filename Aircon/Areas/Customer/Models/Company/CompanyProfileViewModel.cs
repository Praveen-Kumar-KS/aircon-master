using Aircon.Business.Media;
using Aircon.Core.Infrastructure;
using Aircon.ViewModels.Shared;
using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Customer.Models.Company
{
    public class CompanyProfileViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Company Name")]
        public string CompanyName { get; set; }
        public int AddressId { get; set; }
        public AddressViewModel MainAddress { get; set; }
        [UIHint("Picture")]
        public PictureModel Logo { get; set; }
    }
}
