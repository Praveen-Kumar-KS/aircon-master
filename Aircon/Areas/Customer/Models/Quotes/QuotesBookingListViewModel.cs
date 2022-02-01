using Aircon.Areas.Customer.Models.Bookings;
using Aircon.Business.Models.Customer.Bookings;
using Aircon.Business.Models.Customer.Quotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.Quotes
{
    public class QuotesBookingListViewModel
    {
        public List<QuoteModel> Quotes { get; set; }
        public List<BookingModel> Bookings { get; set; }
    }
}
