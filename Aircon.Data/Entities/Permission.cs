using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aircon.Data.Entities
{
    public partial class Permission : AuditableEntity
    {
        public Permission()
        {
            RolePermissions = new List<RolePermission>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SystemName { get; set; }
        [Required]
        public string Category { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}