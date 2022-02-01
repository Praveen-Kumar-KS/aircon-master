using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class GenericAttribute : AuditableEntity
    {
        public int EntityId { get; set; }
        public string KeyGroup { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
