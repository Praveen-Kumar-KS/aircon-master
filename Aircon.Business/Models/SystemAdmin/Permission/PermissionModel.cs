using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.SystemAdmin.Permission
{
    public class PermissionModel
    {
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string Category { get; set; }
    }
}
