using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class Note : AuditableEntity
    {
        public string Text { get; set; }
        public int CreatedById { get; set; }
    }
}
