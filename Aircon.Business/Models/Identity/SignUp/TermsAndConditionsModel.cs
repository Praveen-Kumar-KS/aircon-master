using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Identity.SignUp
{
    public class TermsAndConditionsModel
    {
        public int CustomerOpportunityId { get; set; }
        public bool IsTermsAccepted { get; set; }
    }
}
