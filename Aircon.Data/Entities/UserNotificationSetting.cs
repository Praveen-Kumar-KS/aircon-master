using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aircon.Data.Entities
{
    public class UserNotificationSetting : NotificationSettingEntity
    {
        [ForeignKey("Id")]
        public User User { get; set; }
    }
}
