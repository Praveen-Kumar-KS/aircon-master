using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class ContactEntity 
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int AddressId { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
    }
}
