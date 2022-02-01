using Aircon.Business.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Customer.Contact
{
    public class CustomerContactModel
    {
        public int CustomerId { get; set; }
        public int ContactId { get; set; }
        public int AddressId { get; set; }
        public ContactModel Contact { get; set; }
        public AddressModel Address { get; set; }

    }
}
