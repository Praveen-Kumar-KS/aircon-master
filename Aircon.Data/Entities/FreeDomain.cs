using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class FreeDomain : AuditableEntity
    {
        public string DomainName { get; set; }
    }
}
