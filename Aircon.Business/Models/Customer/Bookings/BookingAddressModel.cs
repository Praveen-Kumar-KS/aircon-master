using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Customer.Bookings
{
    public class BookingAddressModel
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int ContactId { get; set; }
        public BookingAddressType BookingAddressType { get; set; }

    }
}
