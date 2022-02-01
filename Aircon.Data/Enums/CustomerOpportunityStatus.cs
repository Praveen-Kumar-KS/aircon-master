using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum CustomerOpportunityStatus
    {
        [Description("CompanyAddress")]
        CompanyAddress = 0 ,
        [Description("CompanyOtherAddress")]
        CompanyOtherAddress = 1,
        [Description("SubscriptionPlan")]
        SubscriptionPlan = 2,
        [Description("SetupPaymentMethod")]
        SetupPaymentMethod = 3,
        [Description("TermsAndConditions")]
        TermsAndConditions = 4,
        CallbackScheduled = 5,
        Abandoned = 6,
        Approved = 7
    }
}
