using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Customer.Models.Preference
{
    public class NotificationSettingViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }
        [Display(Name = "Templete Definition")]
        public int TemplateDefinitionId { get; set; }
        [Display(Name = "Notification Group")]
        public int NotificationGroupId { get; set; }
        [Display(Name = "User")]
        public int UserId { get; set; }
    }
}
