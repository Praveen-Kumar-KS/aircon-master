using Aircon.Business.Extensions;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.SampleData.Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{

    public class EmployeeDemoSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.Demo;

        public override int Order => 1;

        private readonly AirconDbContext _airconDbContext;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public EmployeeDemoSeed(AirconDbContext airconDbContext, IConfiguration configuration, UserManager<User> userManager)
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

            var userPassword = systemAdminSection["Password"];

            var userList = BogusEmployeeData.GetUsers();
            var userCnt = _airconDbContext.Users.Where(x => x.IsEmployee).ToList().Count;
            if (userCnt < 10)
            {
                foreach (var fakeUser in userList)
                {
                    var user = new User
                    {
                        UserName = fakeUser.Email,
                        DisplayUserId = fakeUser.DisplayUserId,
                        FirstName = fakeUser.FirstName,
                        LastName = fakeUser.LastName,
                        WorkTitle = fakeUser.WorkTitle,
                        CreationDateUtc = fakeUser.CreationDate,
                        ApprovedDateUtc = fakeUser.ApprovedDate,
                        ActivatedDateUtc = fakeUser.ActivatedDate,
                        SignedUpDateUtc = fakeUser.SignedUpDate,
                        IsActive = fakeUser.IsActive,
                        IsApproved = fakeUser.IsApproved,
                        IsEmployee = fakeUser.IsEmployee,
                        UserStatus = fakeUser.UserStatus,
                        Email = fakeUser.Email,
                        PhoneNumber = fakeUser.PhoneNumber
                    };
                    var userCreated = await _userManager.CheckAddNewUserAsync(user, userPassword);
                    var userCreatedRole = await _userManager.AddToRoleAsync(userCreated, fakeUser.Role);
                }
                await _airconDbContext.SaveChangesAsync();

            }
            await _airconDbContext.SaveChangesAsync();

        }
    }

}
