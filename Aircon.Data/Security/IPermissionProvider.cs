using Aircon.Data.Entities;
using Aircon.Framework.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aircon.Data.Security
{
    public interface IPermissionProvider
    {
        IEnumerable<Permission> GetPermissions();
        IEnumerable<DefaultPermission> GetDefaultPermissions();
        IEnumerable<Role> GetRoles();
    }
}
