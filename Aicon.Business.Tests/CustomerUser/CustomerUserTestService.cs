using Aircon.Business.Models.Shared;
using Aircon.Business.Services.Customer;
using Aircon.Data.Entities;
using Aircon.Test;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aicon.Business.UnitTests.CustomerUser
{
    [Collection("AirconWebApplicationFactory")]
    public class CustomerUserTestService : IntegrationTestBase ,IDisposable
    {

        private readonly ICustomerUserService _customerUserService;
        public CustomerUserTestService(AirconWebApplicationFactory factory) : base(factory)
        {
            _customerUserService = GetRequiredService<ICustomerUserService>();
        }
        [Fact]
        public void GetUsersListTest()
        {
            var result = _customerUserService.GetUser(1);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetQueueUsers_Awaiting_Review_ReturnsRecords()
        {
            var result = _customerUserService.GetQueueUsers(3, Aircon.Data.Enums.UserStatus.AwaitingReview, string.Empty, 5); ;
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void GetQueueUsers_Denied_ReturnsRecords()
        {
            var result = _customerUserService.GetQueueUsers(3, Aircon.Data.Enums.UserStatus.Denied, string.Empty, 5);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void GetCustomerUsers_All_ReturnsRecords()
        {
            var result = _customerUserService.GetCustomertUsers(3, true, true, string.Empty, 5);
            var search = result.FirstOrDefault().FirstName;
            var searchresult = _customerUserService.GetCustomertUsers(3, true, true, search, 5);
            Assert.True(searchresult.Count < result.Count);
            Assert.True(result.Count > 0);

        }

        [Fact]
        public void GetCustomerUsers_Active_ReturnsRecords()
        {
            var result = _customerUserService.GetCustomertUsers(4,true, false, string.Empty, 5);
            Assert.True(result.Count > 0);
            var search = result.FirstOrDefault().FirstName;
            var searchresult = _customerUserService.GetCustomertUsers(4, true, false, search, 5);
            Assert.True(searchresult.Count < result.Count);
        }

        [Fact]
        public void GetCustomerUsers_InActive_ReturnsRecords()
        {
            var result = _customerUserService.GetCustomertUsers(3, false, false, string.Empty, 5);
            Assert.True(result.Count > 0);
            var search = result.FirstOrDefault().FirstName;
            var searchresult = _customerUserService.GetCustomertUsers(3, false, false, search, 5);
            Assert.True(searchresult.Count < result.Count);
        }

        [Fact]
        public void AddUserTest()
        {
            var request = new UserModel();
            request.Id = 1;
            request.FirstName = "John";
            request.LastName = "Doe";
            request.Email = "JohnDoe@gmail.com";
            request.WorkTitle = "sales";
            request.PhoneNumber = "9869005690";
            request.Role = "Adminstrator";
            var result = _customerUserService.AddUser(request);
            Assert.NotEqual(0, result.Id);
        }

        [Fact]
        public async void UpdateUserTest()
        {
            var request = new UserModel();
            request.Id = 1;
            request.FirstName = "John";
            request.LastName = "Doe";
            request.Email = "JohnDoe@gmail.com";
            request.WorkTitle = "sales";
            request.PhoneNumber = "76899876678";
            var result = await _customerUserService.UpdateUser(request);
            Assert.Equal(1, result.Id);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("JohnDoe@gmail.com", result.Email);
            Assert.Equal("sales", result.WorkTitle);
            Assert.Equal("76899876678", result.PhoneNumber);
        }
        [Fact]
        public void ActivateUser()
        { 
            var id = 1;
            _customerUserService.ActivatingUser(id);
            var data = _customerUserService.GetUser(id);
            Assert.True(data.IsActive);
        }

        [Fact]
        public void DeactivateUser()
        {
            var id = 1;
            _customerUserService.DeactivatingUser(id);
            var data = _customerUserService.GetUser(id);
            Assert.False(data.IsActive);
        }

        //[Fact]
        //public void AppproveUser()
        //{
        //    var id = 1;
        //    _customerUserService.ApprovingUser(id);
        //    var data = _customerUserService.GetUser(id);
        //    Assert.Equal(true, data.IsApproved);
        //    Assert.Equal("Approved", data.UserStatus);
        //}        

        //[Fact]
        //public void DenyUser()
        //{
        //    var id = 1;
        //    _customerUserService.DenyUser(id);
        //    var data = _customerUserService.GetUser(id);
        //    Assert.Equal(false, data.IsApproved);
        //    Assert.Equal("denied", data.UserStatus);
        //}

        public void Dispose()
        {
           
        }
    }
}
