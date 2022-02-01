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
    public class QuotePricingSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.Demo;

        public override int Order => 6;

        private readonly AirconDbContext _airconDbContext;

        public QuotePricingSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            var quotePrcinglist = QuotesBookingBogusData.GetQuotePricing();
            var quotePricingcnt = _airconDbContext.QuotePricings.ToList().Count;
            if (quotePricingcnt < 10)
            {
                foreach (var fakeQuotePricing in quotePrcinglist)
                {
                    var quotePricing = new QuotePricing
                    {
                        QuoteId = _airconDbContext.Quotes.Select(x => x.Id).FirstOrDefault(),
                        MarkupPercent=fakeQuotePricing.MarkupPercent,
                        ItemName=fakeQuotePricing.ItemName,
                        PricingType=fakeQuotePricing.PricingType,
                        CustomerPrice=fakeQuotePricing.CustomerPrice,
                        Cost= fakeQuotePricing.Cost
                    };
                    _airconDbContext.QuotePricings.Add(quotePricing);
                }
                await _airconDbContext.SaveChangesAsync();

            }
            await _airconDbContext.SaveChangesAsync();

        }
    }
}
