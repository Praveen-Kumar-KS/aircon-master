using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum ShipmentType
    {
        [Description("Door To Door")]
        DoorToAirport = 0,
        [Description("Airport To Airport")]
        AirportToAirport =1
    }
}
