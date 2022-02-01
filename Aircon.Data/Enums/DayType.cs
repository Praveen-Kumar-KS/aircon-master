using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    [Flags]
    public enum DayType : int
    {
        [Description("Today")]
        Today = 0,
        [Description("This Week")]
        ThisWeek = 1,
        [Description("This Month")]
        ThisMonth = 2

    }
}
