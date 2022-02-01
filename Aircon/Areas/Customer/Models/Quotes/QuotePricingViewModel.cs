using Aircon.Areas.Customer.Models.Quotes;
using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.Quotes
{
    public class QuotePricingViewModel
    {
        public string Id { get; set; }
        public int QuoteId { get; set; }
        public PricingType PricingType { get; set; }
        public int DoorTruckingMarkupPercent { get; set; }
        [Display(Name = "Cost")]
        public decimal DoorTruckingCost { get; set; }
        [Display(Name = "Total")]
        public decimal DoorTrucingTotalCost { get; set; }
        public int AirFreightMarkupPercent { get; set; }
        [Display(Name = "Cost")]
        public decimal AirFreightCost { get; set; }
        public string ChargeableName { get; set; }
        public QuoteViewModel Quotes { get; set; }
        [Display(Name = "Total")]
        public decimal AirFreightTotalCost{ get; set; }
        public string AddAdditionalTerms { get; set; }
        public List<ChargeableCost> chargeableList { get; set; }

    }
    public class ChargeableCost
    {
        [Display(Name = "Item Name")]
        public string ChargeableName { get; set; }
        [Display(Name = "Length")]
        public Decimal ChargeableAmount { get; set; }

    }
}
