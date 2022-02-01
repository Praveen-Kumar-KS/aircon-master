using Aircon.Business.Models.SystemAdmin.Permission;
using Aircon.Business.Services.SystemAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.SystemAdmin.Controllers
{
    public class PermissionController : BaseSystemAdminController
    {

        private readonly IPermissionMappingService _permissionMappingService;

        public PermissionController(IPermissionMappingService permissionMappingService)
        {
            _permissionMappingService = permissionMappingService;
        }

        public IActionResult Index()
        {
            var viewModel = _permissionMappingService.GetPermissionMapping();
            return View(viewModel);
        }

        [HttpPost, ActionName("SaveData")]
        public virtual async Task<IActionResult> SaveData(PermissionMappingModel model, IFormCollection form)
        {
            //if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageAcl))
            //    return AccessDeniedView();


            var currentAvailablePermissions = _permissionMappingService.GetPermissionMapping();
            var rolePermissionModel = new List<RolePermissionModel>();


            foreach (var cr in currentAvailablePermissions.AvailableRoles)
            {
                var formKey = "allow_" + cr.RoleId;
                var permissionRecordSystemNamesToRestrict = !StringValues.IsNullOrEmpty(form[formKey])
                    ? form[formKey].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    : new List<string>();

                foreach(var systemName in permissionRecordSystemNamesToRestrict)
                {
                    var permission = currentAvailablePermissions.AvailablePermissions.Where(x => x.SystemName == systemName).SingleOrDefault();
                    rolePermissionModel.Add(new RolePermissionModel { PermissionId = permission.PermissionId, RoleId = cr.RoleId });
                }

            }

            var result = _permissionMappingService.SetPermissionMapping(rolePermissionModel);

            SuccessNotification("Permission Saved");

            return RedirectToAction("Index");
        }

    }
}
