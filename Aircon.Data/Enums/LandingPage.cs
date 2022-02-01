using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://stackoverflow.com/questions/3489453/how-can-i-convert-an-enumeration-into-a-listselectlistitem
namespace Aircon.Data.Enums
{
    [Flags]
    public enum LandingPage : int
    {
        Workflow = 0,
        Metric = 1
    }
}
