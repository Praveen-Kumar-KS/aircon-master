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
    public class QuoteSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.Demo;

        public override int Order => 4;

        private readonly AirconDbContext _airconDbContext;

        public QuoteSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            var quotelist = QuotesBookingBogusData.GetQuotes();
            var quotecnt = _airconDbContext.Quotes.ToList().Count;
            if (quotecnt < 10)
            {
                foreach (var fakequote in quotelist)
                {
                    var quote = new Quote
                    {
                        IsKnownShipper = fakequote.IsKnownShipper,
                        IsDimension = fakequote.IsDimension,
                        ShipmentType = fakequote.ShipmentType,
                        QuoteType = fakequote.QuoteType,
                        MeasurementUnit = fakequote.MeasurementUnit,
                        ArrivesOn = fakequote.ArrivesOn,
                        ServiceLevel = fakequote.ServiceLevel,
                        PickUpDate=fakequote.PickUpDate,
                        PickUpZipCode=fakequote.PickUpZipCode,
                        DeliveryBy=fakequote.DeliveryBy,
                        ShipmentHeaderId= _airconDbContext.ShippingDetails.Select(x => x.Id).FirstOrDefault(),
                        OriginId=fakequote.OriginId,
                        DestinationId=fakequote.DestinationId,
                        CustomerId = _airconDbContext.Customers.Select(x=>x.Id).FirstOrDefault()
                    };
                    _airconDbContext.Quotes.Add(quote);
                }
                await _airconDbContext.SaveChangesAsync();

            }
            await _airconDbContext.SaveChangesAsync();

        }
    }
}
