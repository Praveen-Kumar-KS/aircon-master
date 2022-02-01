using Aircon.Data.Entities;
using Aircon.SampleData.Entity;
using Aircon.Test;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aircon.Data.UnitTest.Entity
{
    [Collection("AirconWebApplicationFactory")]
    public class PermissionConfigurationTests : IntegrationTestBase
    {
        public PermissionConfigurationTests(AirconWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public void TableShouldGetCreated()
        {
            Assert.True(AirconDbContext.Permissions.Any());
        }
        [Fact]
        public void NameIsRequired()
        {
            var permission = SamplePermissionData.PermissionWithNoName;
            AirconDbContext.Permissions.Add(permission);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());
        }
        [Fact]
        public void SystemNameIsRequired()
        {
            var permission = SamplePermissionData.PermissionWithNoSystemName;
            AirconDbContext.Permissions.Add(permission);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());
        }
        [Fact]
        public void CategoryIsRequired()
        {
            var permission = SamplePermissionData.PermissionWithNoCategory;
            AirconDbContext.Permissions.Add(permission);
            Assert.Throws<DbUpdateException>(() => AirconDbContext.SaveChanges());
        }
        [Fact]
        public void AddedItemShouldGetPersisted()
        {
            //arrange 
            var permission = SamplePermissionData.Permission;

            //act
            AirconDbContext.Permissions.Add(permission);
            AirconDbContext.SaveChanges();

            //assert
            Assert.Equal(permission, AirconDbContext.Permissions.Find(permission.Id));
        }
    }
}
