using Aircon.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Business.Extensions
{

    internal static class UserManagerExtensions
    {
        public static async Task<User> CheckAddNewUserAsync(this UserManager<User> userManager, User sysuser, string password)
        {
            var user = await userManager.FindByNameAsync(sysuser.Email);
            if (user != null)
                return user;
            var result = await userManager.CreateAsync(sysuser, password);
            if (!result.Succeeded)
            {
                var errorDescriptions = string.Join("\n", result.Errors.Select(x => x.Description));
                throw new InvalidOperationException(
                    $"Tried to add user {sysuser.UserName}, but failed. Errors:\n {errorDescriptions}");
            }
            user = await userManager.FindByEmailAsync(sysuser.Email);
            return user;
        }

        public static async Task<Role> CheckAddNewRoleAsync(this RoleManager<Role> roleManager, Role role)
        {
            var resultRole = await roleManager.FindByNameAsync(role.Name);
            if (resultRole != null)
                return role;

            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                var errorDescriptions = string.Join("\n", result.Errors.Select(x => x.Description));
                throw new InvalidOperationException(
                    $"Tried to add role {role.Name}, but failed. Errors:\n {errorDescriptions}");
            }

            return role;
        }
        
    }

}
