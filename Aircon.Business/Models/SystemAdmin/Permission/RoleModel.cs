using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.SystemAdmin.Permission
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public bool IsSystemRole { get; set; }
    }
}
