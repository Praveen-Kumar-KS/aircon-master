using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class Address : AuditableEntity
    {
        public string NickName { get; set; } 
        public string Line1 { get; set; } // Req
        public string Line2 { get; set; }
        public string City { get; set; } // Req
        public string State { get; set; } // Req
        public string Zip { get; set; } // Req
        public bool IsActive { get; set; } // defaults to true
        public string SpecialInstruction { get; set; }
    }
}
