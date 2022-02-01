using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Entities
{
    public class TemplateDefinition : AuditableEntity
    {
        public int MaxNameLength { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string TemplateText { get; set; }
        public bool IsLayout { get; set; }
        public string Layout { get; set; }
        public string SampleTemplateText { get; set; }
        public string Instructions { get; set; }
        public string EmailSubjectText { get; set; }
    }
}
