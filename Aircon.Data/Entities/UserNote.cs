using Aircon.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    [Note("UserNote")]
    public class UserNote : NoteEntity
    {
        [ForeignKey("Id")]
        public User User { get; set; }
    }
}
