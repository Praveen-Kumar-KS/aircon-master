using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.SampleData.Bogus;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public class BookingSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.Demo;

        public override int Order => 7;

        private readonly AirconDbContext _airconDbContext;

        public BookingSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            var bookinglist = QuotesBookingBogusData.GetBooking();
            var bookcnt = _airconDbContext.Bookings.ToList().Count;
            if (bookcnt < 10)
            {
                foreach (var fakebooking in bookinglist)
                {
                    var booking = new Booking
                    {
                        CustomerName = fakebooking.CustomerName,
                        FirstName = fakebooking.FirstName,
                        LastName = fakebooking.LastName,
                        Route = fakebooking.Route,
                        Type = fakebooking.Type,
                        Volume = fakebooking.Volume,
                        Quantity = fakebooking.Quantity,
                        ArrivesOn = fakebooking.ArrivesOn,
                        CutOffTime = fakebooking.CutOffTime,
                        QuoteId = _airconDbContext.Quotes.Select(x => x.Id).FirstOrDefault(),
                        UserId =fakebooking.UserId,
                        AddressId=fakebooking.AddressId
                    };
                    _airconDbContext.Bookings.Add(booking);
                }
                await _airconDbContext.SaveChangesAsync();

            }
            await _airconDbContext.SaveChangesAsync();

        }
    }
}
