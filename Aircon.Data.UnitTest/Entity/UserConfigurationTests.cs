using Aircon.Data.Entities;
using Aircon.Test;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Aircon.SampleData.Entity;
using System.Diagnostics;
using AutoMapper.Configuration.Annotations;

namespace Aircon.Data.UnitTest.Entity
{
    [Collection("AirconWebApplicationFactory")]
    public class UserConfigurationTests : IntegrationTestBase
    {
        private readonly UserManager<User> _userManager;
        private readonly AirconDbContext _airconDbContext;
        public UserConfigurationTests(AirconWebApplicationFactory factory) : base(factory)
        {
            _userManager = AirconServiceProvider.GetRequiredService<UserManager<User>>();
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
            Assert.True(AirconDbContext.Users.Any());
        }
        [Fact(Skip = "Signup Process doesn't require")]
        public void FirstNameIsRequired()
        {
            var newUser = SampleUserData.GetUserWithNoFirstName();
            var result = _userManager.CreateAsync(newUser, "Aircon@123");
            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => AirconDbContext.SaveChanges());
        }
        [Fact(Skip = "Signup Process doesn't require")]
        public void LastNameIsRequired()
        {
            var newUser = SampleUserData.GetUserWithNoLastName();
            var result = _userManager.CreateAsync(newUser, "Aircon@123");
            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => AirconDbContext.SaveChanges());

        }
        [Fact(Skip = "Signup Process doesn't require")]
        public void WorkTitleIsRequired()
        {
            var newUser = SampleUserData.GetUserWithNoWorkTitle();
            var result = _userManager.CreateAsync(newUser, "Aircon@123");
            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => AirconDbContext.SaveChanges());
        }
        [Fact]
        public void AddedUserdGetPersisted()
        {
            var newUser = SampleUserData.GetUser();
            var result = _userManager.CreateAsync(newUser,"Aircon@123");
            foreach(var error in result.Result.Errors)
            {
                Console.WriteLine(error.Description);
            }
            Assert.True(result.Result.Succeeded);
        }
    }
}
