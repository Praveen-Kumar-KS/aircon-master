using System;

namespace Aircon.Data.Entities
{
    public class AuditableEntity : BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

    }
}
