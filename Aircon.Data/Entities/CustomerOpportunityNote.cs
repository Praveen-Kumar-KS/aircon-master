using Aircon.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    [Note("CustomerOpportunityNote")]
    public class CustomerOpportunityNote : NoteEntity
    {
        [ForeignKey("Id")]
        public CustomerOpportunity CustomerOpportunity { get; set; }
    }
}
