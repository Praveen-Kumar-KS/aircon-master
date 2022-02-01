using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Models
{
    [Flags]
    public enum GridActionConfirmType : int
    {
        DenyUser = 0,
        ApproveUser = 1,
        ActivateCustomer = 2,
        ActivatingUser = 3,
        DeactivateCustomer = 4,
        DeactivatingUser = 5,
        DenyCustomerOpportunity = 6,
        DeleteContact = 7,
        ApproveCustomer = 8,
        DeleteCustomerOpportunity=9,
        ActivateEmployeeUser =10,
        DeactivateEmployeeUser=11,
        DeleteAddress=12
    }

    public class GridAction
    {
        public GridActionConfirmType GridActionConfirmType { get; set; }
        public string Title { get; set; }
        public string AdditionalText { get; set; }
        public string ActionButtonText { get; set; }
        public string CancelButtonText { get; set; }
    }
}
