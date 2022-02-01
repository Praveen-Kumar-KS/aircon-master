using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.SampleData.Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public class BookingAddressSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 1;

        private readonly AirconDbContext _airconDbContext;

        public BookingAddressSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            var bookingAddresslist = QuotesBookingBogusData.GetBookingAddress();
            var bookingAddresscnt = _airconDbContext.BookingAddresses.ToList().Count;
            if (bookingAddresscnt < 10)
            {
                foreach (var fakebooking in bookingAddresslist)
                {
                    var bookingAddress = new BookingAddress
                    {
                        ContactId = fakebooking.ContactId,
                        BookingId = fakebooking.BookingId,
                        BookingAddressType = fakebooking.BookingAddressType,
                        
                    };
                    _airconDbContext.BookingAddresses.Add(bookingAddress);
                }
                await _airconDbContext.SaveChangesAsync();

            }
            await _airconDbContext.SaveChangesAsync();

        }
    }
}
