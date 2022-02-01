using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class NotificationSettingEntity : AuditableEntity
    {
        public bool IsActive { get; set; }
        public int NotificationSettingId { get; set; }
        [ForeignKey("NotificationSettingId")]
        public NotificationSetting NotificationSetting { get; set; }
    }
}
