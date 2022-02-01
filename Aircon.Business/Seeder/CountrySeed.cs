using Aircon.Business.Extensions;
using Aircon.Core.Data;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Aircon.Data.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public class CountrySeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 2;

        private readonly AirconDbContext _airconDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CountrySeed(AirconDbContext airconDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _airconDbContext = airconDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public override async Task SeedAsync()
        {
            try
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "data", "countries_with_timeZones.json");//  "/assets/data/countries_with_timeZones.json");
                string data = File.ReadAllText(path);
                List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(data);
                foreach (var country in countries)
                {
                    var result = _airconDbContext.Countries.Where(x => x.CountryName == country.CountryName).SingleOrDefault();
                    if (result == null)
                    {
                        _airconDbContext.Countries.Add(country);
                    }
                }
                await _airconDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
