using Aircon.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    [Note("BookingNote")]
    public class BookingNote : NoteEntity
    {
        [ForeignKey("Id")]
        public Booking Booking { get; set; }
    }
}
