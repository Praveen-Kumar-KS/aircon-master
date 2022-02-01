using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.SystemAdmin.Permission
{
    public class PermissionMappingModel
    {

        public PermissionMappingModel()
        {
            AvailablePermissions = new List<PermissionModel>();
            AvailableRoles = new List<RoleModel>();
            Allowed = new Dictionary<string, IDictionary<int, bool>>();
        }
        
        public IList<PermissionModel> AvailablePermissions { get; set; }

        public IList<RoleModel> AvailableRoles { get; set; }

        //[permission system name] / [role id] / [allowed]
        public IDictionary<string, IDictionary<int, bool>> Allowed { get; set; }

    }
}
