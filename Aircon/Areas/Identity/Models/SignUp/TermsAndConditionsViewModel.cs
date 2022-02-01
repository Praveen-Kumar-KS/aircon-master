using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Identity.Models.SignUp
{
    public class TermsAndConditionsViewModel
    {
        public int CustomerOpportunityId { get; set; }
        public bool IsTermsAccepted { get; set; }
    }
}
