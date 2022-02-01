using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class CustomerContact : ContactEntity
    {
        [ForeignKey("Id")]
        public Customer Customer { get; set; }
    }
}
