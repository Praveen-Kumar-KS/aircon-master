using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum QuoteMeasurementUnit
    {
        [Description("Inches/Kgs")]
        InchByKgs = 0,
        [Description("Cms/Lbs")]
        CentimeterByPound = 1
    }
}
