using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Shared
{
    public class OpportunityPaymentMethodModel : PaymentMethodModel
    {
        public int CustomerOpportunityId { get; set; }
    }
}
