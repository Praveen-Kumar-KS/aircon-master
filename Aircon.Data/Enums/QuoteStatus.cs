using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum QuoteStatus : int
    {
        [Description("In Progress")]
        InProgress = 0,
        [Description("Drafts")]
        Drafts = 1,
        [Description("Closed")]
        Closed = 2
    }
}
