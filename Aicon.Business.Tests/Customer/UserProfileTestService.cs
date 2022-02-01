using Aircon.Business.Media;
using Aircon.Business.Models.Customer.Profile;
using Aircon.Business.Seeder;
using Aircon.Business.Services.Customer;
using Aircon.Extensions;
using Aircon.SampleData.Bogus;
using Aircon.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aicon.Business.UnitTests.Customer
{
    [Collection("AirconWebApplicationFactory")]
    public class UserProfileTestService : IntegrationTestBase, IDisposable
    {
        private readonly IUserProfileService _userProfileService;
        private Aircon.Data.Entities.User TestUser;


        public UserProfileTestService (AirconWebApplicationFactory factory) : base(factory)
        {
            _userProfileService = GetRequiredService<IUserProfileService>();
            TestUser = BogusCustomerData.GetUsers().FirstOrDefault();
            AirconDbContext.Users.Add(TestUser);
            AirconDbContext.SaveChanges();
        }

        [Fact]
        public void Get_UserProfile()
        {
            var result = _userProfileService.GetUserProfile(TestUser.Id);
            Assert.Equal(TestUser.Id, result.Id);
            Assert.Equal(TestUser.UserName, result.UserName);
        }

        [Fact]
        public void Save_Profile()
        {
            var save = _userProfileService.GetUserProfile(1).ToViewModel();
            save.FirstName = "Yvette";
            save.LastName = "Doyle";
            save.WorkTitle = "Manager";
            save.Email = "Yvette78@Kayla.64.biz";

            var saveresult = _userProfileService.SaveUserProfile(save.ToModel());
            Assert.Equal(save.Id, saveresult.Id);
            Assert.Equal(save.FirstName, saveresult.FirstName);
            Assert.Equal(save.LastName, saveresult.LastName);
            Assert.Equal(save.WorkTitle, saveresult.WorkTitle);
            Assert.Equal(save.Email, saveresult.Email);
        }

        public void Dispose()
        {
            AirconDbContext.Users.Remove(TestUser);
        }
    }
}
