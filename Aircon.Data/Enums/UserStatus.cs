using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Aircon.Data.Enums
{
    [Flags]
    public enum UserStatus : int
    {
        [Description("Awaiting Review")]
        AwaitingReview = 0,
        Denied = 1,
        Approved = 2
    }
}
