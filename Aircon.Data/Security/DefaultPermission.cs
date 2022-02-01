using Aircon.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aircon.Framework.Security
{
    public class DefaultPermission
    {
        public DefaultPermission()
        {
            this.Permissions = new List<Permission>();
        }
        public string RoleSystemName { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }

}