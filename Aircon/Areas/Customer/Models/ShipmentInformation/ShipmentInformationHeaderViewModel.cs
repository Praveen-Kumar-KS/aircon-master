using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.ShipmentInformation
{
    public class ShipmentInformationHeaderViewModel
    {
        public int Id { get; set; }
        [Display(Name = "TotalNo Of Pieces")]
        public int TotalNoOfPieces { get; set; }
        [Display(Name = "Total Volumetric Weight")]
        public decimal TotalVolumetricWeight { get; set; }
        [Display(Name = "Total Chargeable Weight")]
        public decimal TotalChargeableWeight { get; set; }
        [Display(Name = "Total Volume")]
        public decimal TotalVolume { get; set; }
        [Display(Name = "Total Chargeable Volume")]
        public decimal TotalChargeableVolume { get; set; }
        public int QuoteId { get; set; }
    }
}
