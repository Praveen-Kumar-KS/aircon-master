using Aircon.Business.Extensions;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Aircon.Data.Security;
using Aircon.SampleData.Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public class SystemUserSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 1;

        private readonly AirconDbContext _airconDbContext;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public SystemUserSeed(AirconDbContext airconDbContext, IConfiguration configuration, UserManager<User> userManager)
        {
            _airconDbContext = airconDbContext;
            _configuration = configuration;
            _userManager = userManager;
        }

        public override async Task SeedAsync()
        {
            var systemAdminSection = _configuration.GetSection("SystemAdmin");
            if (systemAdminSection == null)
                return;

            var userEmail = systemAdminSection["Email"];
            var userPassword = systemAdminSection["Password"];
            var user = new User
            {
                CreationDateUtc = DateTime.UtcNow,
                Deleted = false,
                Disabled = false,
                Email = userEmail,
                FirstName = "System",
                LastName = "Admin",
                UserName = userEmail,
                EmailConfirmed = true,
                UserStatus = UserStatus.Approved
            };
            var superUser = await _userManager.CheckAddNewUserAsync(user, userPassword);
            var addToRole = await _userManager.AddToRoleAsync(superUser, RoleSystemName.SystemAdministrators);
            var addToRole1 = await _userManager.AddToRoleAsync(superUser, RoleSystemName.Administrators);
            await _airconDbContext.SaveChangesAsync();

        }
    }

}
