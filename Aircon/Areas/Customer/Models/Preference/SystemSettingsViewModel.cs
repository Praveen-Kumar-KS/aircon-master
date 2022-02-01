using Aircon.Business.Models.Customer.Preference;
using System.Collections.Generic;

namespace Aircon.Areas.Customer.Models.Preference
{
    public class SystemSettingsViewModel
    {
        public int Id { get; set; }
        public int PreferenceId { get; set; }
        public List<NotificationSettingModel> NotificationSettings { get; set; }

    }
}
