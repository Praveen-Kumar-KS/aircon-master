using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class BookingAddress : AuditableEntity
    {
        public int BookingId { get; set; }
        public int ContactId { get; set; }
        public BookingAddressType BookingAddressType { get; set; }
        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }

    }
}
