using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class QuotePricing: AuditableEntity
    {
        public int QuoteId { get; set; }
        public PricingType PricingType { get; set; }
        public int MarkupPercent { get; set; }
        public decimal Cost { get; set; }
        public decimal CustomerPrice { get; set; }
        public string ItemName { get; set; }

        [ForeignKey("QuoteId")]
        public Quote Quote { get; set; }
        public string AddAdditionalTerms { get; set; }
    }
}
