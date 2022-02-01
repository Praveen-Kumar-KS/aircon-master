using Aircon.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
//https://weblog.west-wind.com/posts/2015/feb/10/back-to-basics-utc-and-timezones-in-net-web-apps
namespace Aircon.Data.Entities
{
    [Index(nameof(IsEmployee))]
    [Index(nameof(IsActive))]
    public class User : IdentityUser<int>
    {
        public string DisplayUserId { get; set; } // Req
        public string FirstName { get; set; } // Req
        public string LastName { get; set; } // Req
        public string WorkTitle { get; set; } // Req
        public bool Disabled { get; set; } 
        public bool Deleted { get; set; }
        public int? CustomerId { get; set; }
        public int? CustomerOpportunityId { get; set; }
        public DateTime? CreationDateUtc { get; set; }
        public DateTime? ApprovedDateUtc { get; set; }
        public DateTime? ActivatedDateUtc { get; set; }
        public DateTime? SignedUpDateUtc { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public bool IsEmployee { get; set; }
        public UserStatus UserStatus { get; set; } // AwaitingReview

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("CustomerOpportunityId")]
        public CustomerOpportunity CustomerOpportunity { get; set; }
        public ICollection<UserNotificationSetting> UserNotificationSettings { get; set; }

        public int? PreferenceId { get; set; }
        [ForeignKey("PreferenceId")]
        public Preference UserPreference { get; set; }

        public int? AvatarId { get; set; }
        [ForeignKey("AvatarId")]
        public Attachment Avatar { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public User()
        {
            UserRoles = new List<UserRole>();
            UserNotificationSettings = new List<UserNotificationSetting>();
            UserNotes = new List<UserNote>();
        }
        public virtual ICollection<UserNote> UserNotes { get; set; }

    }
   
}
