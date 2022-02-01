using System.ComponentModel.DataAnnotations;

namespace Aircon.Data.Entities
{
    public class LookupEntity : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
