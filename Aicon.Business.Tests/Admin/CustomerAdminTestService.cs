using Aircon.Areas.Admin.Models.Customer;
using Aircon.Business.Models.Admin.Customer;
using Aircon.Business.Services.Admin;
using Aircon.Test;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aicon.Business.UnitTests.Admin
{
    [Collection("AirconWebApplicationFactory")]
    public class AdminCustomerTestService : IntegrationTestBase, IDisposable
    {

        private readonly ICustomerAdminService _adminCustomerService;
        public AdminCustomerTestService(AirconWebApplicationFactory factory) : base(factory)
        {
            _adminCustomerService = GetRequiredService<ICustomerAdminService>();
        }

        [Fact]
        public void GetCustomerOpportunities_Callback_Scheduled_ReturnsRecords()
        {
            var result = _adminCustomerService.GetCustomerOpportunities(Aircon.Data.Enums.CustomerOpportunityStatus.CallbackScheduled, string.Empty,5);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void GetCustomerOpportunities_Abandoned_ReturnsRecords()
        {
            var result = _adminCustomerService.GetCustomerOpportunities(Aircon.Data.Enums.CustomerOpportunityStatus.Abandoned, string.Empty,5);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void GetCustomers_All_ReturnsRecords()
        {
            var result = _adminCustomerService.GetCustomers(true, true, string.Empty,5);
            var search = result.FirstOrDefault().AdminEmail;
            var searchresult = _adminCustomerService.GetCustomers(true, true, search,5);
            Assert.True(searchresult.Count < result.Count);
            Assert.True(result.Count > 0);

        }

        [Fact]
        public void GetCustomers_Active_ReturnsRecords()
        {
            var result = _adminCustomerService.GetCustomers(true, false, string.Empty,5);
            Assert.False(result.Count > 0);
            var search = result.FirstOrDefault().AdminEmail;
            var searchresult = _adminCustomerService.GetCustomers(true, false, search,5);
            Assert.True(searchresult.Count < result.Count);
        }

        [Fact]
        public void GetCustomers_InActive_ReturnsRecords()
        {
            var result = _adminCustomerService.GetCustomers(false, false, string.Empty,5);
            Assert.False(result.Count > 0);
            var search = result.FirstOrDefault().CompanyName;
            var searchresult = _adminCustomerService.GetCustomers(false, false, search,5);
            Assert.True(searchresult.Count < result.Count);
        }
        [Fact]
        public void GetCustomers_Detail_Records()
        {
            var result = _adminCustomerService.GetCustomer(1);
            Assert.Equal(1, result.CustomerId);
        }
        [Fact]
        public void GetCustomerOpportunities_Detail_Records()
        {
            var customerOppurtunity = _adminCustomerService.GetCustomerOpportunity(7);
            Assert.Equal(7, customerOppurtunity.Id);
        }
        [Fact]
        public void Update_Existing_Customer()
        {
            var request = new CustomerAdminModel();
            request.CustomerId = 7;
            request.AdminName = "Test";
            request.CompanyName = "Weimann and Sons";
            request.AdminEmail = "Test@yahoo.com";          
            var result = _adminCustomerService.UpdateCustomer(request);
            Assert.Equal(7, result.CustomerId);
            Assert.Equal("Test", result.AdminName);
            Assert.Equal("Weimann and Sons", result.CompanyName);
            Assert.Equal("Test@yahoo.com", result.AdminEmail);
            
        }
        [Fact]
        public void Update_Existing_CustomerOpportunity()
        {
            var request = new CustomerOpportunityAdminModel();
            var requestcredit = new CustomerAdminModel();
            request.CustomerOpportunityId = 7;
            request.AdminName = "Test Customer";
            request.CompanyName = "Weimann and Sons";
            request.AdminEmail = "Kailee12@yahoo.com";
            requestcredit.Creditlimit = '0';
            var result = _adminCustomerService.UpdateCustomerOpportunity(request);
            var result2 = _adminCustomerService.UpdateCustomer(requestcredit);
            Assert.Equal(1, result.CustomerOpportunityId);
            Assert.Equal("Test Customer", result.AdminName);
            Assert.Equal("Weimann and Sons", result.CompanyName);
            Assert.Equal("Kailee12@yahoo.com", result.AdminEmail);
            Assert.Equal('0', result2.Creditlimit);
           
        }
        public void Dispose()
        {

        }
        [Fact]
        public void Activate_Existing_Customer()
        {
            var id = 7;
            _adminCustomerService.ActivateCustomer(id);
            var result = _adminCustomerService.GetCustomer(id);
            Assert.True(result.IsActive);

        }
        [Fact]
        public void DeActivate_Existing_Customer()
        {
            var id = 7;
            _adminCustomerService.ActivateCustomer(id);
            var result = _adminCustomerService.GetCustomer(id);
            Assert.True(result.IsActive);
        }
        //[Fact]
        //public void Deny_Existing_CustomerOpportunity()
        //{
        //    var id = 7;
        //    _adminCustomerService.DenyCustomerOpportunity(id);
        //    var result = _adminCustomerService.GetCustomerOpportunity(id);
        //   // Assert.Equal("Abandoned", result.Status);
        //}
        [Fact]
        public void Delete_Existing_CustomerOpportunity()
        {
            var Id = 5;
            _adminCustomerService.DeleteCustomerOpportunity(Id);
            var data = _adminCustomerService.GetCustomerOpportunity(Id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public void Delete_Existing_CustomerProfile()
        {
            var Id = 5;
            _adminCustomerService.DeleteCustomerProfile(Id);
            var data = _adminCustomerService.GetCustomer(Id);
            Assert.IsType<NotFoundResult>(data);
        }

    }
}
