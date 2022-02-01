using Aircon.Data.Entities;
using Aircon.SampleData.Entity;
using Aircon.Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aircon.Data.UnitTest.Entity
{
    [Collection("AirconWebApplicationFactory")]
    public class CustomerConfigurationTests : IntegrationTestBase
    {
        private readonly AirconDbContext _airconDbContext;
        public CustomerConfigurationTests(AirconWebApplicationFactory factory) : base(factory)
        {
            _airconDbContext = factory.Services.GetRequiredService<AirconDbContext>();
        }
       
        [Fact]
        public void TestCustomerData()
        {
            Assert.True(AirconDbContext.Permissions.Any());
        }

        [Fact]
        public void TableShouldGetCreated()
        {
            Assert.False(AirconDbContext.Customers.Any());
        }
        [Fact]
        public void CompanyNameIsRequired()
        {
            var newCustomer = SampleCustomerData.GetCustomerWithCompanyName();
            AirconDbContext.Customers.Add(newCustomer);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());
        }
        [Fact]
        public void FranchiseParentIsRequired()
        {
            var newCustomer = SampleCustomerData.GetCustomerWithFranchiseParent();
            AirconDbContext.Customers.Add(newCustomer);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());

        }
        [Fact]
        public void AdminEmailIsRequired()
        {
            var newCustomer = SampleCustomerData.GetCustomerWithAdminEmail();
            AirconDbContext.Customers.Add(newCustomer);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());

        }
        [Fact]
        public void AdminNameIsRequired()
        {
            var newCustomer = SampleCustomerData.GetCustomerWithAdminName();
            AirconDbContext.Customers.Add(newCustomer);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());

        }

        [Fact]
        public void AdminPhoneNumberIsRequired()
        {
            var newCustomer = SampleCustomerData.GetCustomerWithAdminPhoneNumber();
            AirconDbContext.Customers.Add(newCustomer);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());

        }
        [Fact]
        public void AlternateEmailIsRequired()
        {
            var newCustomer = SampleCustomerData.GetCustomerWithAlternateEmail();
            AirconDbContext.Customers.Add(newCustomer);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());

        }
        [Fact]
        public void IATANumberIsRequired()
        {
            var newCustomer = SampleCustomerData.GetCustomerWithIATANumber();
            AirconDbContext.Customers.Add(newCustomer);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());

        }
        [Fact]
        public void GetCustomerWithEinOrSsnIsRequired()
        {
            var newCustomer = SampleCustomerData.GetCustomerWithEinOrSsn();
            AirconDbContext.Customers.Add(newCustomer);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());

        }
       
    }
}
