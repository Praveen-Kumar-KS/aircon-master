using Aircon.Core.Caching;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Data.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Aircon.Business.Services.Security
{
    public partial interface IPermissionService
    {
        Task<bool> Authorize(Permission permission);
        Task<bool> Authorize(Permission permission, User user);
        Task<bool> Authorize(string permissionRecordSystemName);
        Task<bool> Authorize(string permissionRecordSystemName, User user);
    }

    public partial class PermissionService : IPermissionService
    {
        private const string PERMISSIONS_ALLOWED_KEY = "Aircon.permission.allowed-{0}-{1}";
        private const string PERMISSIONS_PATTERN_KEY = "Aircon.permission.";

        private readonly AirconDbContext _airconDBContext;
        private readonly IStaticCacheManager _cacheManager;
        private readonly UserManager<User> _userManager;



        public PermissionService(AirconDbContext airconDbContext, IStaticCacheManager cacheManager, UserManager<User> userManager)
        {
            _airconDBContext = airconDbContext;
            _cacheManager = cacheManager;
            _userManager = userManager;
        }

        public static CacheKey PermissionAllowedCacheKey => new CacheKey("Air.permissionrecord.allowed.{0}-{1}");
        //public static string PermissionAllowedPrefix => "Air.permissionrecord.allowed.{0}";


        protected virtual async Task<bool> Authorize(string permissionRecordSystemName, string userRole)
        {
            if (string.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            //string key = string.Format(PERMISSIONS_ALLOWED_KEY, userRole, permissionRecordSystemName);
            var key = _cacheManager.PrepareKeyForDefaultCache(PermissionAllowedCacheKey, permissionRecordSystemName, userRole);

            //return true;
            return await _cacheManager.GetAsync<bool>(key, async () =>
            {
                var permissionRecord = await _airconDBContext.Permissions.Include(x => x.RolePermissions).ThenInclude(role => role.Role).FirstOrDefaultAsync(x => x.SystemName == permissionRecordSystemName);
                return permissionRecord?.RolePermissions.Any(x => x.Role.Name == userRole) ?? false;
            });
        }

        public virtual async Task<bool> Authorize(Permission permission)
        {
            var user = await GetCurrentUserAsync();//  _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return false;
            }
            return await Authorize(permission, user);
        }
        public virtual async Task<bool> Authorize(Permission permission, User user)
        {
            if (permission == null)
                return false;

            if (user == null)
                return false;

            return await Authorize(permission.SystemName, user);
        }
        public virtual async Task<bool> Authorize(string permissionRecordSystemName)
        {
            var user = await GetCurrentUserAsync();  //await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return false;
            }
            return await Authorize(permissionRecordSystemName, user);
        }
        public virtual async Task<bool> Authorize(string permissionRecordSystemName, User user)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                if (await Authorize(permissionRecordSystemName, role))
                    //yes, we have such permission
                    return true;
            }
            return false;
        }
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContextHelper.Current.User);

    }
}
