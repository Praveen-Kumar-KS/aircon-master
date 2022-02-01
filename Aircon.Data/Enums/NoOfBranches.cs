using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Aircon.Data.Enums
{
    [Flags]
    public enum NoOfBranches : int
    {
        [Description("1-5")]
        OneToFive = 0,
        [Description("6-10")]
        SixToTen = 1,
        [Description("11-20")]
        ElevenToTwenty =2 ,
        [Description("Above 20")]
        AboveTwenty = 3

    }
}
