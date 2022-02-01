using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum ShipmentStatus : int
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Deliverd")]
        Delivered = 1,
        [Description("Recently Cancelled")]
        RecentlyCancelled = 2
    }
}
