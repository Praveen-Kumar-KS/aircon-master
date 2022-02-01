using Aircon.Business.Seeder;
using Aircon.Business.Services.Admin;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.SampleData.Bogus;
using Aircon.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aicon.Business.UnitTests.Admin
{
    [Collection("AirconWebApplicationFactory")]
    public class AdminUserTestService : IntegrationTestBase, IDisposable
    {

        private readonly IAdminUserService _adminUserService;

        List<Aircon.Data.Entities.Customer> Customers = new List<Aircon.Data.Entities.Customer>();

        public AdminUserTestService(AirconWebApplicationFactory factory) : base(factory)
        {
            _adminUserService = GetRequiredService<IAdminUserService>();
            var customers = BogusCustomerData.GetCustomer(10);
            foreach(var customer in customers)
            {
                AirconDbContext.Customers.Add(customer);
            }
            AirconDbContext.SaveChanges();
            Customers = customers;
        }

        [Fact]
        public void GetQueueUsers_Awaiting_Review_ReturnsRecords()
        {
            var result = _adminUserService.GetQueueUsers(Aircon.Data.Enums.UserStatus.AwaitingReview, string.Empty,5); ;
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void GetQueueUsers_Denied_ReturnsRecords()
        {
            var result = _adminUserService.GetQueueUsers(Aircon.Data.Enums.UserStatus.Denied, string.Empty,5); ;
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void GetAdminUsers_All_ReturnsRecords()
        {
            var result = _adminUserService.GetAdminUsers(true,true, string.Empty,5);
            var search = result.FirstOrDefault().FirstName;
            var searchresult = _adminUserService.GetAdminUsers(true, true, search,1);
            Assert.True(searchresult.Count < result.Count);
            Assert.True(result.Count > 0);

        }

        [Fact]
        public void GetAdminUsers_Active_ReturnsRecords()
        {
            var result = _adminUserService.GetAdminUsers(true, false, string.Empty,5);
            Assert.True(result.Count > 0);
            var search = result.FirstOrDefault().FirstName;
            var searchresult = _adminUserService.GetAdminUsers(true, false, search,5);
            Assert.True(searchresult.Count < result.Count);
        }

        [Fact]
        public void GetAdminUsers_InActive_ReturnsRecords()
        {
            var result = _adminUserService.GetAdminUsers(false, false, string.Empty,5);
            Assert.True(result.Count > 0);
            var search = result.FirstOrDefault().FirstName;
            var searchresult = _adminUserService.GetAdminUsers(false, false, search,5);
            Assert.True(searchresult.Count < result.Count);
        }


        [Fact]
        public async Task UpdateUser_Should_UpdateData()
        {
            var testCustomer = Customers[3];
            var testUser = testCustomer.Users.FirstOrDefault();
            var testUserModel = _adminUserService.GetUser(testUser.Id);
            testUserModel.FirstName = "TestFirstName";
            var result = await _adminUserService.UpdateUser(testUserModel);
            Assert.Equal("TestFirstName", result.FirstName);
        }

        public void Dispose()
        {
           foreach(var customer in Customers)
            {
                AirconDbContext.Customers.Remove(customer);
            }
            AirconDbContext.SaveChanges();
        }


    }
}
