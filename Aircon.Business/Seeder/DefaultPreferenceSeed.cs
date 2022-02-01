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

    public class DefaultPreferenceSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 2;

        private readonly AirconDbContext _airconDbContext;

        public DefaultPreferenceSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            try
            {

                var systemSetting = _airconDbContext.SystemSettings.Include(x => x.DefaultNotificationSettings).Include(x => x.DefaultPreference).FirstOrDefault();
                var defaultTimezoneValue = _airconDbContext.Countries.Where(x => x.IsoAlpha3 == "USA").Include(x => x.WindowsTimeZones).Select(x => new {
                    Id = x.Id,
                    TimeZoneId = x.WindowsTimeZones.Where(x => x.Name == "Central Standard Time").SingleOrDefault().Id
                }).SingleOrDefault();

                if (systemSetting != null)
                {
                    if (systemSetting.DefaultPreference == null)
                    {
                        systemSetting.DefaultPreference
                            = new Preference
                            {
                                LandingPage = LandingPage.Workflow,
                                MeasurementUnit = MeasurementUnit.Metric,
                                CountryId = defaultTimezoneValue.Id,
                                WindowsTimeZoneId = defaultTimezoneValue.TimeZoneId
                            };
                    }
                    if (systemSetting.DefaultNotificationSettings.FirstOrDefault() == null)
                    {
                        systemSetting.DefaultNotificationSettings = SeedDefaultNotificationSetting.GetDefaultNotificationSettings();
                    }
                    _airconDbContext.SystemSettings.Update(systemSetting);

                }
                else
                {
                    // SystemSetting is null
                    systemSetting = new SystemSetting
                    {
                        DefaultPreference = new Preference
                        {
                            LandingPage = LandingPage.Workflow,
                            MeasurementUnit = MeasurementUnit.Metric,
                            CountryId = defaultTimezoneValue.Id,
                            WindowsTimeZoneId = defaultTimezoneValue.TimeZoneId
                        },
                        DefaultNotificationSettings = SeedDefaultNotificationSetting.GetDefaultNotificationSettings()
                    };

                    _airconDbContext.SystemSettings.Add(systemSetting);
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
