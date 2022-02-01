using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.ShipmentInformation
{
    public class ShipmentInformationDetailViewModel 
    {
        public int Id { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Weight")]
        public decimal Weight { get; set; }
        [Display(Name = "Volume")]
        public decimal Volume { get; set; }
        [Display(Name = "Length")]
        public decimal Length { get; set; }
        [Display(Name = "Width")]
        public decimal Width { get; set; }
        [Display(Name = "Height")]
        public decimal Height { get; set; }
        public int QuoteId { get; set; }
    }
}
