using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum PricingType 
    {
        [Description("DoorToTrucking")]
        DoorTrucking = 0,
        [Description("AirFrieght")]
        Airport = 1,
        [Description("Additional Cost")]
        ChargeableCost = 2
    }
}
