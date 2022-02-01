using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class Booking : AuditableEntity
    {
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Route { get; set; }
        public string Type { get; set; }
        public decimal ChargeableWeight { get; set; }
        public decimal Volume { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ArrivesOn { get; set; }
        public DateTime CutOffTime { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public int? QuoteId { get; set; }
        [ForeignKey("QuoteId")]
        public Quote Quote { get; set; }
        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address MainAddress { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public virtual ICollection<BookingNote> BookingNotes { get; set; }

    }
}
