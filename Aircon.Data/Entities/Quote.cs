using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class Quote : AuditableEntity
    {
        public int? OriginId { get; set; }
        public int? DestinationId { get; set; }
        public int? ShipmentHeaderId { get; set; }
        public DateTime ArrivesOn { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? DeliveryBy { get; set; }
        public string PickUpZipCode { get; set; }
        public bool IsKnownShipper { get; set; }
        public bool IsDimension { get; set; }
        public int? CustomerId { get; set; }      
        public ServiceLevel ServiceLevel { get; set; }
        public QuoteType QuoteType { get; set; }
        public QuoteStatus QuoteStatus { get; set; }
        public ShipmentType ShipmentType { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("OriginId")]
        public Airport Origin { get; set; }
        [ForeignKey("DestinationId")]
        public Airport Destination { get; set; }
        [ForeignKey("ShipmentHeaderId")]
        public ShipmentInformationHeader ShipmentInformationHeader { get; set; }
        public virtual ICollection<ShipmentInformationDetail> ShipmentInformationDetails { get; set; }

    }
}
