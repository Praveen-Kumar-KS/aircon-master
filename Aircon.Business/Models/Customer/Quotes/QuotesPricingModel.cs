using Aircon.Business.Models.Customer.Quotes;
using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Customer.Quotes
{
    public class QuotesPricingModel
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public PricingType PricingType { get; set; }
        public int DoorTruckingMarkupPercent { get; set; }
        public decimal DoorTruckingCost { get; set; }
        public decimal DoorTrucingTotalCost { get; set; }
        public int AirFreightMarkupPercent { get; set; }
        public decimal AirFreightCost { get; set; }
        public string ChargeableName { get; set; }
        public QuoteModel Quotes { get; set; }
        public decimal AirFreightTotalCost { get; set; }
        public string AddAdditionalTerms { get; set; }
        public List<ChargeableCost> chargeableList { get; set; }

    }
    public class ChargeableCost
    {
        public string ChargeableName { get; set; }
        public Decimal ChargeableAmount { get; set; }

    }

}
