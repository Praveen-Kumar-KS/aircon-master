using Aircon.Data.Entities;

namespace Aircon.Business.Models.Customer.Preference
{
    public class NotificationSettingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int TemplateDefinitionId { get; set; }        
        public int NotificationGroupId { get; set; }     
        public int UserId { get; set; }
    }
}
