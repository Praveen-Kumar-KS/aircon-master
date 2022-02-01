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
    public class BookingNotificationSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 1;

        private readonly AirconDbContext _airconDbContext;

        public BookingNotificationSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            var bookingNotificationlist = QuotesBookingBogusData.GetBookingDocMaster();
            var bookingNotificationcnt = _airconDbContext.BookingNotifications.ToList().Count;
            if (bookingNotificationcnt < 10)
            {
                foreach (var fakebookingNotification in bookingNotificationlist)
                {
                    var bookingNotification = new BookingNotification
                    {
                        BookingId = fakebookingNotification.BookingId,
                        ContactId = fakebookingNotification.AttachmentId

                    };
                    _airconDbContext.BookingNotifications.Add(bookingNotification);
                }
                await _airconDbContext.SaveChangesAsync();

            }
            await _airconDbContext.SaveChangesAsync();

        }
    }
}
