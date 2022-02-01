using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Customer.ShipmentInformation
{
    public class ShipmentInformationDetailModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public int QuoteId { get; set; }
    }
}
