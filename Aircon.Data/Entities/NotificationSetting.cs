using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class NotificationSetting : AuditableEntity
    {
        public string Name { get; set; }
        public string SystemName { get; set; }
        public int TemplateDefinitionId { get; set; }
        [ForeignKey("TemplateDefinitionId")]
        public TemplateDefinition TemplateDefinition { get; set; }
        public NotificationGroup NotificationGroup { get; set; }
    }
}
