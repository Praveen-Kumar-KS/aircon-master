using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    [Flags]
    public enum AccountType : int
    {
        Checking = 0,
        Savings = 1
    }
}
