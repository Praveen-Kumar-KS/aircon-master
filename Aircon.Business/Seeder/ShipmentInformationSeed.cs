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
    public class ShipmentInformationSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.Demo;

        public override int Order => 3;

        private readonly AirconDbContext _airconDbContext;

        public ShipmentInformationSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            var shipmentInformationList = QuotesBookingBogusData.GetShipmentInformationHeader();
            var shipmentInformationCnt = _airconDbContext.ShippingDetails.ToList().Count;
            if (shipmentInformationCnt < 10)
            {
                foreach (var fakeshipmentdata in shipmentInformationList)
                {
                    var shipmentInformationheader = new ShipmentInformationHeader
                    {
                        TotalNoOfPieces = fakeshipmentdata.TotalNoOfPieces,
                        TotalVolume = fakeshipmentdata.TotalVolume,
                        TotalChargeableVolume = fakeshipmentdata.TotalChargeableVolume,
                        TotalChargeableWeight = fakeshipmentdata.TotalChargeableWeight,
                        TotalVolumetricWeight = fakeshipmentdata.TotalVolumetricWeight,
                        //QuoteId = fakeshipmentdata.QuoteId

                    };
                    _airconDbContext.ShippingDetails.Add(shipmentInformationheader);
                }
            }
            await _airconDbContext.SaveChangesAsync();

        }
    }
}
