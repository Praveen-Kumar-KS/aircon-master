using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class SystemSetting : AuditableEntity
    {
        public ICollection<DefaultNotificationSetting> DefaultNotificationSettings { get; set; }
        public int PreferenceId { get; set; }
        [ForeignKey("PreferenceId")]
        public Preference DefaultPreference { get; set; }
        public SystemSetting()
        {
            DefaultNotificationSettings = new List<DefaultNotificationSetting>();
        }
    }
}
