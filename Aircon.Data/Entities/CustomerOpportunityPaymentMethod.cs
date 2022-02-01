using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class CustomerOpportunityPaymentMethod : PaymentMethodEntity
    {
        [ForeignKey("Id")]
        public CustomerOpportunity CustomerOpportunity { get; set; }
    }
}
