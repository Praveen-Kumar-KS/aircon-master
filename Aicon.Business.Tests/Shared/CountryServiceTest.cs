using Aircon.Business.Services.Shared;
using Aircon.SampleData.Bogus;
using Aircon.Test;
using System;
using Xunit;

namespace Aicon.Business.UnitTests.Shared
{
    [Collection("AirconWebApplicationFactory")]
    public class CountryTestService : IntegrationTestBase, IDisposable
    {
        private readonly ICountryService _countryService;

        public CountryTestService(AirconWebApplicationFactory factory) : base(factory)
        {
            _countryService = GetRequiredService<ICountryService>();
        }

        [Fact]
        public void Get_Country_List()
        {
            var result = _countryService.GetCountryList();
            Assert.Equal(139, result.Count);
        }

        [Fact]
        public void Get_TimeZone_List()
        {
            var time = _countryService.GetTimeZoneList(1);
            Assert.Equal(1, time.Count);            
        }

        public void Dispose()
        {
        }
    }

}

