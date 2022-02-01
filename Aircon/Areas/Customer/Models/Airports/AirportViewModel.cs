using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.Airports
{
    public class AirportViewModel
    {
        public int Id { get; set; }
        public string Ident { get; set; }
        [Display(Name = "Name")]
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
