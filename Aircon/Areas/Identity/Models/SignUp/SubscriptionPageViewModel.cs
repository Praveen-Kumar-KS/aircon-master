using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Identity.Models.SignUp
{
    public class SubscriptionPageViewModel
    {
        public NoOfBranches NoOfBranches { get; set; }
        public int SubscriptionTypeId { get; set; }
        public int CustomerOpportunityId { get; set; }

    }
}
