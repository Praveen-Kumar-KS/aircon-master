using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Customer.Bookings
{
    public class BookingDocumentMasterModel
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public int AttachmentId { get; set; }
        public int BookingId { get; set; }
        public BookingDocumentType BookingDocumentType { get; set; }
    }
}
