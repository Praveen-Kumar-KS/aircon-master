using System;
using System.Collections.Generic;

namespace Aircon.Business.Models.Shared
{
    public class NoteModel
    {        
        public int NoteId { get; set; }
        public string Text { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
