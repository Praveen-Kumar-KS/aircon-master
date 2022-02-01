using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class Country : AuditableEntity
    {
        public Country()
        {
            WindowsTimeZones = new List<WindowsTimeZone>();
        }
        public string CountryName { get; set; }
        public string IsoAlpha3 { get; set; }
        public string IsoAlpha2 { get; set; }
        public List<WindowsTimeZone> WindowsTimeZones { get; set; }
    }
}
