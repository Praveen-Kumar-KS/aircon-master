using System.Collections.Generic;

namespace Aircon.Business.Models.Customer.Preference
{
    public class SystemSettingModel
    {
        public int Id { get; set; }
        public int PreferenceId { get; set; }
        public List<NotificationSettingModel> NotificationSettings { get; set; }

    }
}
