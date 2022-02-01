using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum BookingStatus : int
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Recently Cancelled")]
        RecentlyCancelled = 1
    }
}
