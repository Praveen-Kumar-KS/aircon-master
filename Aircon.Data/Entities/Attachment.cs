using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Aircon.Data.Entities
{
    public class Attachment : AuditableEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string MimeType { get; set; }
        public long Size { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

    }    
}
