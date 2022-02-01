using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    [Flags]
    public enum MeasurementUnit : int
    {
        [Description("Metric")]
        Metric = 0,
        [Description("Imperial")]
        Imperial = 1,
        [Description("Inches/Kgs")]
        InchesOrKgs = 2,
        [Description("Cms/Lbs")]
        Centimeter = 3
    }
}
