using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aircon.SampleData.Entity;
using Aircon.Data.Entities;

namespace Aircon.SampleData.Entity
{
    public static class SamplePermissionData
    {

        public static readonly Permission PermissionWithNoName = new Permission {  SystemName = "SystemAdmin", Category = "SystemAdmin" };
        public static readonly Permission PermissionWithNoSystemName = new Permission { Name = "Access admin", Category = "Admin" };
        public static readonly Permission PermissionWithNoCategory = new Permission { Name = "Admin. Manage User Profiles", SystemName = "UserProfile" };
        public static readonly Permission Permission = new Permission { Name = "Admin. Manage Roles", SystemName = "Role", Category = "UserManagement" };

    }
}
