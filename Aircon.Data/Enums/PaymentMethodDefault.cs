using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    [Flags]
    public enum PaymentMethodDefault : int
    {
        Subscription = 0,
        Shipping = 1,
        [Description("Subscription And Shipping")]
        SubscriptionAndShipping = 3
    }
}
