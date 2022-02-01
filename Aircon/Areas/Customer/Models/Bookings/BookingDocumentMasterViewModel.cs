using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Models.Bookings
{
    public class BookingDocumentMasterViewModel
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public int AttachmentId { get; set; }
        public int BookingId { get; set; }
        public BookingDocumentType BookingDocumentType { get; set; }
    }
}
