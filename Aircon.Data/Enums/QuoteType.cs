using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum QuoteType
    {
        [Description("Quote By Volume")]
        QuoteByVolume = 0,
        [Description("Itemized")]
        Itemized = 1
    }
}
