using Aircon.Business.Models.SystemAdmin.Permission;
using Aircon.Data;
using Aircon.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Services.SystemAdmin
{
    public interface IPermissionMappingService
    {
        PermissionMappingModel GetPermissionMapping();
        List<RolePermissionModel> SetPermissionMapping(List<RolePermissionModel> rolePermissionModel);
    }


    public class PermissionMappingService : IPermissionMappingService
    {
        private readonly AirconDbContext _airconDbContext;
        
        public PermissionMappingService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public PermissionMappingModel GetPermissionMapping()
        {
            var model = new PermissionMappingModel();
            model.AvailableRoles = _airconDbContext.Roles.Select(x => new RoleModel {
                RoleId = x.Id,
                Name = x.Name,
                DisplayName = x.DisplayName,
                IsSystemRole = x.IsSystemRole,
                Active = x.Active
            }).ToList();

            
            var permissionList = _airconDbContext.Permissions.Include(x => x.RolePermissions).ToList();


            foreach (var permission in permissionList)
            {
                model.AvailablePermissions.Add(new PermissionModel
                {
                    PermissionId = permission.Id,
                    Name = permission.Name,
                    SystemName = permission.SystemName,
                    Category = permission.Category
                });

                foreach (var role in model.AvailableRoles)
                {
                    if (!model.Allowed.ContainsKey(permission.SystemName))
                        model.Allowed[permission.SystemName] = new Dictionary<int, bool>();
                    model.Allowed[permission.SystemName][role.RoleId] =
                        permission.RolePermissions.Any(x => x.RoleId == role.RoleId);
                }
            }
            return model;
        }

        public List<RolePermissionModel> SetPermissionMapping(List<RolePermissionModel> rolePermissionModel)
        {
            //TODO - REMOVE THE KEYS FROM THE CACHE AFTER SETTING NEW PERMISSION.
            //TODO - CHECK THE ROLE AND PERMISSION SEED
            
            var currentRolePermissions = _airconDbContext.RolePermissions.ToList();
            var newRolePermissions = rolePermissionModel.Select(x => new RolePermission { PermissionId = x.PermissionId, RoleId = x.RoleId }).ToList();

            var deleteRolePermissions = currentRolePermissions.Except(newRolePermissions);
            foreach(var p in deleteRolePermissions)
            {
                _airconDbContext.RolePermissions.Remove(p);
            }
            var addRolePermissions = newRolePermissions.Except(currentRolePermissions);
            foreach (var p in addRolePermissions)
            {
                _airconDbContext.RolePermissions.Add(p);
            }
            _airconDbContext.SaveChanges();
            return rolePermissionModel;
        }
    }
}
