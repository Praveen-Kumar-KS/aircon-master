using System.ComponentModel.DataAnnotations;

namespace Aircon.ViewModels.Shared
{
    public class LookupViewModel
    {
        public int LookupId { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Active")]
        public bool Active { get; set; }
    }

}
