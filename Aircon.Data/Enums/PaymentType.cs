using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    [Flags]
    public enum PaymentType : int
    {
        [Description("Credit Card")]
        CreditCard = 0,
        ACH = 1,
    }
}
