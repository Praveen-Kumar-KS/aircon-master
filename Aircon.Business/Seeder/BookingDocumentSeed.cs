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
    public class BookingDocumentSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 1;

        private readonly AirconDbContext _airconDbContext;

        public BookingDocumentSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            var bookingDoclist = QuotesBookingBogusData.GetBookingDocMaster();
            var bookingDoccnt = _airconDbContext.BookingDocumentMasters.ToList().Count;
            if (bookingDoccnt < 10)
            {
                foreach (var fakebookingDoc in bookingDoclist)
                {
                    var bookingDoc = new BookingDocumentMaster
                    {
                        DocumentName = fakebookingDoc.DocumentName,
                        BookingId = fakebookingDoc.BookingId,
                        //AttachmentId = fakebookingDoc.AttachmentId,
                        BookingDocumentType = fakebookingDoc.BookingDocumentType

                    };
                    _airconDbContext.BookingDocumentMasters.Add(bookingDoc);
                }
                await _airconDbContext.SaveChangesAsync();

            }
            await _airconDbContext.SaveChangesAsync();

        }
    }
}
