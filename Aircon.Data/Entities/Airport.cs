using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class Airport: AuditableEntity
    {
        public string Ident { get; set; }
        public string Name { get; set; }
        public string LatitudeDeg { get; set; }
        public string LongitudeDeg { get; set; }
        public string ElevationFt { get; set; }
        public string Continent { get; set; }
        public string IsoCountry { get; set; }
        public string IsoRegion { get; set; }
        public string Municipality { get; set; }
        public string GpsCode { get; set; }
        public string IataCode { get; set; }
        public string LocalCode { get; set; }

    }
}
