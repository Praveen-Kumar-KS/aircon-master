using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class Preference : AuditableEntity
    {
        public LandingPage LandingPage { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public int CountryId { get; set; }
        public int WindowsTimeZoneId { get; set; }
        public Country Country { get; set; }
        public WindowsTimeZone WindowsTimeZone { get; set; }
    }
}
