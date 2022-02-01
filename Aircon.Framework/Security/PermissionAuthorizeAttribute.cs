using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Aircon.Business.Services.Security;
using Aircon.Data.Security;

namespace Aircon.Framework.Security
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PermissionAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public PermissionAuthorizeAttribute(string permission)
        {
            Permission = permission;
        }

        // Get or set the permision property by manipulating
        public string Permission { get; set; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //ignore filter (the action available even when navigation is not allowed)
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(Permission))
                return;
            var permissionService = context.HttpContext.RequestServices.GetRequiredService<IPermissionService>();
            //check whether current customer has access to a public store
            if (await permissionService.Authorize(Permission))
                return;

            //authorize permission of access to the admin area
            if (!await permissionService.Authorize(StandardPermissionProvider.AccessSystemAdmin))
                context.Result = new RedirectToActionResult("AccessDenied", "Security", new { Area = "Identity",  pageUrl = context.HttpContext.Request.Path });
            return;
        }
    }

}
