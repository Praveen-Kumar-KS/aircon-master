using Aircon.Business.Extensions;
using Aircon.Data;
using Aircon.Data.Entities;
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
    public class FreeDomainsSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 3;

        private readonly AirconDbContext _airconDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FreeDomainsSeed(AirconDbContext airconDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _airconDbContext = airconDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public override async Task SeedAsync()
        {
            try
            {
                if (!(_airconDbContext.FreeDomains.ToList().Count > 100))
                {
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "data", "freemail.txt");//  "/assets/data/countries_with_timeZones.json");
                    string line;
                    StreamReader file = new StreamReader(path);
                    if ((line = file.ReadLine()) != null)
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            _airconDbContext.FreeDomains.Add(new FreeDomain { DomainName = line });
                        }
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
