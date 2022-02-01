using Aircon.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    [Note("CustomerNote")]
    public class CustomerNote : NoteEntity
    {
        [ForeignKey("Id")]
        public Customer Customer { get; set; }
    }
}
