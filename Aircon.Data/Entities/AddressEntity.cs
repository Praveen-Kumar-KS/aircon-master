using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class AddressEntity
    {
        public int Id { get; set; }
        public int AddressTypeId { get; set; } // Req
        public int AddressId { get; set; }

        [ForeignKey("AddressTypeId")]
        public AddressType AddressType { get; set; } // Warehouse, Office
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

    }
}
