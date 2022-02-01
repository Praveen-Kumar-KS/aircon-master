using Aircon.Business.Models.Shared;
using Aircon.Business.Seeder;
using Aircon.Business.Services.SystemAdmin;
using Aircon.Extensions;
using Aircon.SampleData.Bogus;
using Aircon.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aicon.Business.UnitTests.SystemAdmin
{
    [Collection("AirconWebApplicationFactory")]
    public class EmployeeUserTestService : IntegrationTestBase, IDisposable
    {
        private readonly IEmployeeUserService _employeeuserService;
        List<Aircon.Data.Entities.User> TestEmployee = new List<Aircon.Data.Entities.User>();

        public EmployeeUserTestService(AirconWebApplicationFactory factory) : base(factory)
        {
            _employeeuserService = GetRequiredService<IEmployeeUserService>();
            var Employees = BogusCustomerData.GetUsers(20);
            foreach (var employee in Employees)
            {
                AirconDbContext.Users.Add(employee);
            }
            AirconDbContext.SaveChanges();
            TestEmployee = Employees;
        }

        [Fact]
        public void GetEmployeeUsers_All_ReturnsRecords()
        {
            var result = _employeeuserService.GetEmployees(true, true, string.Empty, 5);
            Assert.True(result.Count > 0);
            var search = result.FirstOrDefault().FirstName;
            var searchresult = _employeeuserService.GetEmployees(true, true, search, 1);
            Assert.True(searchresult.Count < result.Count);
        }

        [Fact]
        public void GetEmployeeUsers_Active_ReturnsRecords()
        {
            var result = _employeeuserService.GetEmployees(true, false, string.Empty, 5);
            Assert.True(result.Count > 0);
            var search = result.FirstOrDefault().FirstName;
            var searchresult = _employeeuserService.GetEmployees(true, false, search, 5);
            Assert.True(searchresult.Count < result.Count);
        }

        [Fact]
        public void GetAdminUsers_InActive_ReturnsRecords()
        {
            var result = _employeeuserService.GetEmployees(false, false, string.Empty, 5);
            Assert.True(result.Count > 0);
            var search = result.FirstOrDefault().FirstName;
            var searchresult = _employeeuserService.GetEmployees(false, false, search, 5);
            Assert.True(searchresult.Count < result.Count);
        }

        [Fact]
        public void Get_User_Employee()
        {
            var test = TestEmployee[5];
            var result = _employeeuserService.GetEmployee(test.Id);
            Assert.Equal(test.Id, result.Id);
            Assert.Equal(test.FirstName, result.FirstName);
        }

        [Fact]
        public void Activate_User_Employee()
        {
             var id = 1;
            _employeeuserService.ActivateEmployeeUser(id);
            var data = _employeeuserService.GetEmployee(id);
            Assert.True(data.IsActive);
        }

        [Fact]
        public void Deactivate_User_Employee()
        {
            var id = 1;
            _employeeuserService.DeactivateEmployeeUser(id);
             var data = _employeeuserService.GetEmployee(id);
            Assert.False(data.IsActive);
        }

        [Fact]
        public void Add_User_Employee()
        {
            var employeerequest = new UserModel();
            employeerequest.UserName = "SysAdmin";
            employeerequest.FirstName = "System";
            employeerequest.LastName = "Admin";
            employeerequest.Email = "sysadmin123@valueglobal.net";
            employeerequest.WorkTitle = "Admin";
            employeerequest.PhoneNumber = "9784563210";
            employeerequest.CustomerId = null;
            var addemployeeresult = _employeeuserService.AddUser(employeerequest);
            Assert.NotEqual(1, addemployeeresult.Id);
        }

        [Fact]
        public async void Update_User_Employee()
        {
            var test = TestEmployee[5];
            var save = _employeeuserService.GetEmployee(test.Id).ToViewModel();
            save.UserName = "TestUser";
            save.FirstName = "Yvette";
            save.LastName = "Doyle";
            save.WorkTitle = "Manager";
            save.Email = "Yvette78@Kayla.64.biz";
            save.PhoneNumber = "9647538641";
            var result = await _employeeuserService.UpdateUser(save.ToModel());
            Assert.Equal(save.Id, result.Id);
            Assert.Equal(save.UserName, result.UserName);
            Assert.Equal(save.FirstName, result.FirstName);
            Assert.Equal(save.LastName, result.LastName);
            Assert.Equal(save.WorkTitle, result.WorkTitle);
            Assert.Equal(save.Email, result.Email);
            Assert.Equal(save.PhoneNumber, result.PhoneNumber);            
        }

        public void Dispose()
        {
            foreach (var employee in TestEmployee)
            {
                AirconDbContext.Users.Remove(employee);
            }
            AirconDbContext.SaveChanges();
        }
    }
}
