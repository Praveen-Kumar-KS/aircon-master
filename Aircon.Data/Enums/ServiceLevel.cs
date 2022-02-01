using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum ServiceLevel
    {
        [Description("Standard")]
        Standard = 0,
        [Description("Expedited")]
        Expedited = 1,
    }
}
