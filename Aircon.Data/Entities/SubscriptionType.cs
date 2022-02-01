using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class SubscriptionType :  AuditableEntity
    {
        public string Name { get; set; }
        public int MonthlyAmount { get; set; }
        public int AnnualAmount { get; set; }
        public bool IsPopular { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public string Line5 { get; set; }
        public int DisplayOrder { get; set; }
    }
}
