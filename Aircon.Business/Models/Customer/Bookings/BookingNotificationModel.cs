using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Customer.Bookings
{
    public class BookingNotificationModel
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int BookingId { get; set; }
        public int NotificationSettingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
