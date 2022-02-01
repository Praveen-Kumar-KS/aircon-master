using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class ShipmentInformationHeader : AuditableEntity
    {
        public int TotalNoOfPieces { get; set; }
        public decimal TotalVolumetricWeight { get; set; }
        public decimal TotalChargeableWeight { get; set; }
        public decimal TotalVolume { get; set; }
        public decimal TotalChargeableVolume { get; set; }

    }
    public class ShipmentInformationDetail : AuditableEntity
    {
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public int QuoteId { get; set; }
        [ForeignKey("QuoteId")]
        public virtual Quote Quote { get; set; }
    }
}
