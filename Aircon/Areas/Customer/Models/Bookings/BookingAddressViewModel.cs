using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.Bookings
{
    public class BookingAddressViewModel
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int ContactId { get; set; }
        public BookingAddressType BookingAddressType { get; set; }
    }
}
