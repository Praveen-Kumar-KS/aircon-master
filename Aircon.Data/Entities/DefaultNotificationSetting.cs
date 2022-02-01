using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aircon.Data.Entities
{
    public class DefaultNotificationSetting : NotificationSettingEntity
    {
        [ForeignKey("Id")]
        public SystemSetting SystemSetting { get; set; }
    }
}
