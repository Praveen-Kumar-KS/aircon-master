using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class BookingDocumentMaster : AuditableEntity
    {
        public string DocumentName { get; set; }
        public int AttachmentId { get; set; }
        public BookingDocumentType BookingDocumentType { get; set; }
        [ForeignKey("AttachmentId")]
        public Attachment Attachment { get; set; }
        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }
       
    }
}
