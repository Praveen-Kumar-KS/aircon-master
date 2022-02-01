using Aircon.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.Contact
{
    public class CustomerContactViewModel
    {
        public int CustomerId { get; set; }
        public int ContactId { get; set; }
        public int AddressId { get; set; }
        public ContactViewModel Contact { get; set; }
        public AddressViewModel Address { get; set; }
    }

    public class CustomerContactListViewModel
    {        
        public List<CustomerContactViewModel> Contacts { get; set; }
    }
}
