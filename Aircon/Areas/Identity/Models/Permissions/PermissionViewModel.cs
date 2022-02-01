using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Identity.Models.Permissions
{
    public class PermissionViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "System Name")]
        public string SystemName { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }
        public List<RolePermissionViewModel> RolePermissions { get; set; }
       
    }
}
