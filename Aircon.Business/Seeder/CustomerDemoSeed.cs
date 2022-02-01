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

    public class CustomerDemoSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.Demo;

        public override int Order => 2;

        private readonly AirconDbContext _airconDbContext;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public CustomerDemoSeed(AirconDbContext airconDbContext, IConfiguration configuration, UserManager<User> userManager)
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

            var customerCnt = _airconDbContext.Customers.ToList().Count;
            if (customerCnt < 10)
            {
                var customerList = BogusCustomerData.GetCustomer(20);
                foreach (var fakeCustomer in customerList)
                {
                    var domain = fakeCustomer.CustomerDomains.FirstOrDefault().DomainName.ToLower();
                    foreach (var user in fakeCustomer.Users)
                    {
                        user.Email = string.Format("{0}{1}", user.Email, domain);
                        user.UserName = user.Email;
                        var userCreated = await _userManager.CheckAddNewUserAsync(user, userPassword);
                        var userCreatedRole = await _userManager.AddToRoleAsync(userCreated, BogusCustomerData.GetRole());
                    }
                    _airconDbContext.Customers.Add(fakeCustomer);
                }

                var customerOpportunityList = BogusCustomerData.GetCustomerOpportunity();
                foreach (var fakeCustomerOpportunity in customerOpportunityList)
                {
                    var domain = BogusCustomerData.GetDomain();
                    foreach (var user in fakeCustomerOpportunity.Users)
                    {
                        user.Email = string.Format("{0}{1}", user.Email, domain);
                        user.UserName = user.Email;
                        var userCreated = await _userManager.CheckAddNewUserAsync(user, userPassword);
                        var userCreatedRole = await _userManager.AddToRoleAsync(userCreated, BogusCustomerData.GetRole());
                    }

                    _airconDbContext.CustomerOpportunities.Add(fakeCustomerOpportunity);
                }
                await _airconDbContext.SaveChangesAsync();

            }

        }
    }

}
