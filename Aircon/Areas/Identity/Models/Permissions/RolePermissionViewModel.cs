using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Identity.Models.Permissions
{
    public class RolePermissionViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
    }
}
