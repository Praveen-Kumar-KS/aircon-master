using Aircon.Business.Extensions;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Data.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public class RolesAndPermissionsSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 1;

        private readonly AirconDbContext _airconDbContext;
        private readonly IPermissionProvider _permissionProvider;
        private readonly RoleManager<Role> _roleManager;

        public RolesAndPermissionsSeed(AirconDbContext airconDbContext, IPermissionProvider permissionProvider, RoleManager<Role> roleManager)
        {
            _airconDbContext = airconDbContext;
            _permissionProvider = permissionProvider;
            _roleManager = roleManager;
        }

        public override async Task SeedAsync()
        {
            var permissions = _permissionProvider.GetPermissions();
            foreach (var permission in permissions)
            {
                try
                {
                    var result = _airconDbContext.Permissions.Where(x => x.SystemName == permission.SystemName).SingleOrDefault();
                    if (result == null)
                    {
                        _airconDbContext.Permissions.Add(permission);
                    }
                }catch(Exception ex)
                {
                    throw;
                }

                _airconDbContext.SaveChanges();
            }
            var roles = _permissionProvider.GetRoles();
            foreach (var role in roles)
            {
                var result = await _roleManager.CheckAddNewRoleAsync(role);
            }
            _airconDbContext.SaveChanges();
            foreach (var role in roles)
            {
                var result = _airconDbContext.Roles.Where(x => x.Name == role.Name).Include(role => role.RolePermissions).SingleOrDefault();
                if (result != null)
                {
                    var defaultPermission = _permissionProvider.GetDefaultPermissions().Where(x => x.RoleSystemName == role.Name).SingleOrDefault();
                    foreach (var permission2 in defaultPermission.Permissions)
                    {
                        var rolePermission = result.RolePermissions.Where(x => x.Permission.SystemName == permission2.SystemName).SingleOrDefault();
                        if (rolePermission == null)
                        {
                            var permission1 = _airconDbContext.Permissions.Where(x => x.SystemName == permission2.SystemName).SingleOrDefault();
                            result.RolePermissions.Add(new RolePermission { Permission = permission1, RoleId = result.Id });
                        }
                    }
                    _airconDbContext.Roles.Update(result);
                }
                await _airconDbContext.SaveChangesAsync();
            }

        }
    }
}
