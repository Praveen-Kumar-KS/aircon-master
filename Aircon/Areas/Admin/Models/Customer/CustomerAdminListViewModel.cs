using System.Collections.Generic;

namespace Aircon.Areas.Admin.Models.Customer
{
    public class CustomerAdminListViewModel
    {
        public List<CustomerOpportunityAdminViewModel> CustomerOpportunities { get; set; }
        public List<CustomerAdminViewModel> Customers { get; set; }
    }
}
