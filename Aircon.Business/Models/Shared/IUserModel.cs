using Aircon.Core.Data;
using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Shared
{
    public class UserModel : BaseModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string WorkTitle { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string DisplayUserId { get; set; } // Req
        public DateTime? CreationDateUtc { get; set; }
        public DateTime? ApprovedDateUtc { get; set; }
        public DateTime? ActivatedDateUtc { get; set; }
        public DateTime? SignedUpDateUtc { get; set; }
        public bool IsApproved { get; set; }
        public bool IsEmployee { get; set; }
        public UserStatus UserStatus { get; set; } // AwaitingReview
        public string Role { get; set; }
        public int? CustomerId { get; set; }
        public string CompanyName { get; set; }
    }
}
