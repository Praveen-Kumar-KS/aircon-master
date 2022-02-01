using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.SampleData.Bogus
{
    public class FakeUser
    {
        public string DisplayUserId { get; set; } // Req
        public string FirstName { get; set; } // Req
        public string LastName { get; set; } // Req
        public string WorkTitle { get; set; } // Req
        public bool Disabled { get; set; }
        public bool Deleted { get; set; }
        public int? CustomerId { get; set; }
        public int? CustomerOpportunityId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ActivatedDate { get; set; }
        public DateTime? SignedUpDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public bool IsEmployee { get; set; }
        public UserStatus UserStatus { get; set; } // AwaitingReview
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
