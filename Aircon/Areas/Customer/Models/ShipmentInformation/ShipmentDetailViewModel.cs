using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.ShipmentInformation
{
    public class ShipmentDetailViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Weight")]
        public decimal Weight { get; set; }
        [Display(Name = "Volume")]
        public decimal Volume { get; set; }
    }
}
