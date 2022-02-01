using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class NoteEntity
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        [ForeignKey("NoteId")]
        public Note Note { get; set; }
    }
}
