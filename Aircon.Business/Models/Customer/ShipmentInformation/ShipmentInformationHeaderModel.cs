using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Customer.ShipmentInformation
{
    public class ShipmentInformationHeaderModel
    {
        public int Id { get; set; }
        public int TotalNoOfPieces { get; set; }
        public decimal TotalVolumetricWeight { get; set; }
        public decimal TotalChargeableWeight { get; set; }
        public decimal TotalVolume { get; set; }
        public decimal TotalChargeableVolume { get; set; }
        public int QuoteId { get; set; }
    }
}
