using Aircon.Business.Models.Shared;
using Aircon.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Aircon.Business.Services.Shared
{
    public interface ICountryService
    {
        List<IdNamePair> GetCountryList();
        List<IdNamePair> GetTimeZoneList(int countryId);
    }
    public class CountryService : ICountryService
    {
        private readonly AirconDbContext _airconDbContext;
        public CountryService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
    
        }

        public List<IdNamePair> GetCountryList()
        {
            return _airconDbContext.Countries.Select(x => new IdNamePair { Id = x.Id, Name = x.IsoAlpha3 + '-' + x.CountryName }).ToList();
        }

        public List<IdNamePair> GetTimeZoneList(int countryId)
        {
            var timeZones = _airconDbContext.Countries.Where(x => x.Id == countryId).Include(x => x.WindowsTimeZones).SingleOrDefault().WindowsTimeZones.Select(x => new IdNamePair { Id = x.Id,  Name = x.Name + '-' + x.Description }).ToList();
            return timeZones;
        }

    }
}
