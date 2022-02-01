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
    public class AirportSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 1;

        private readonly AirconDbContext _airconDbContext;

        public AirportSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            var airportlist = QuotesBookingBogusData.GetAirport();
            var airportcnt = _airconDbContext.Airports.ToList().Count;
            if (airportcnt < 10)
            {
                foreach (var fakeairportdata in airportlist)
                {
                    var airport = new Airport
                    {
                        Ident = fakeairportdata.Ident,
                        Name = fakeairportdata.Name,
                        LongitudeDeg = fakeairportdata.LongitudeDeg,
                        LatitudeDeg = fakeairportdata.LatitudeDeg,
                        ElevationFt = fakeairportdata.ElevationFt,
                        Continent = fakeairportdata.Continent,
                        IsoRegion = fakeairportdata.IsoRegion,
                        IsoCountry = fakeairportdata.IsoCountry,
                        Municipality = fakeairportdata.Municipality,
                        GpsCode = fakeairportdata.GpsCode,
                        IataCode = fakeairportdata.IataCode,
                        LocalCode = fakeairportdata.LocalCode
                    };
                    _airconDbContext.Airports.Add(airport);
                }
                await _airconDbContext.SaveChangesAsync();

            }
            await _airconDbContext.SaveChangesAsync();

        }
    }

}
