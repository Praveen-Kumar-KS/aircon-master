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
    public class ShipmentInformationDetailSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.Demo;

        public override int Order => 5;

        private readonly AirconDbContext _airconDbContext;

        public ShipmentInformationDetailSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {            
            var shipmentInformationDetails = QuotesBookingBogusData.GetShipmentInformationDetail();
            var shipmentInformationDetailCnt = _airconDbContext.ShipmentInformationDetails.ToList().Count;
            if (shipmentInformationDetailCnt < 10)
            {
                foreach (var fakeshipmentdetaildata in shipmentInformationDetails)
                {
                    var shipmentInformationDetail = new ShipmentInformationDetail
                    {
                        Quantity = fakeshipmentdetaildata.Quantity,
                        Volume = fakeshipmentdetaildata.Volume,
                        Weight = fakeshipmentdetaildata.Weight,
                        Width = fakeshipmentdetaildata.Width,
                        Length = fakeshipmentdetaildata.Length,
                        Height = fakeshipmentdetaildata.Height,
                        QuoteId = _airconDbContext.Quotes.Select(x=>x.Id).FirstOrDefault()

                    };
                    _airconDbContext.ShipmentInformationDetails.Add(shipmentInformationDetail);
                }
                await _airconDbContext.SaveChangesAsync();

            }
            await _airconDbContext.SaveChangesAsync();

        }
    }
}
