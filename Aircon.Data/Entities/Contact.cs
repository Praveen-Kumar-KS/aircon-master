using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class Contact : AuditableEntity
    {
        public string FirstName { get; set; } // Req
        public string LastName { get; set; } //Req
        public string CompanyName { get; set; } //Req
        public string Title { get; set; } //Req
        public string Department { get; set; } 
        public string PhoneNumber { get; set; } //Req
        public string Email { get; set; } //Req
        public bool Active { get; set; }
        public string SpecialInstruction { get; set; }
    }

}
