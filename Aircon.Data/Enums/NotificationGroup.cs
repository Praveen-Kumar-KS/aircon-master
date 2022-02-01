using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    [Flags]
    public enum NotificationGroup : int
    {
        [Description("QUOTES EXPIRING")]
        QuotesExpiring = 0,
        [Description("BOOKING NEEDS ATTENTION")]
        BookingNeedsAttention = 1,
        [Description("SHIPMENT NEEDS ATTENTION")]
        ShipmentNeedsAttention = 2,
        [Description("SHIPMENT DELIVERED")]
        ShipmentDelayed = 3,
        [Description("SHIPMENT DASHBOARD NOTIFICATIONS")]
        ShipmentDashboardNotification = 4
    }
}
