using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
            UserRoles = new List<UserRole>();
            RolePermissions = new List<RolePermission>();
        }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public bool IsSystemRole { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }

    public class UserRole : IdentityUserRole<int>
    {
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

    }

}
