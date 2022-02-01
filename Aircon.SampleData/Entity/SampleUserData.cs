using Aircon.Data.Entities;
using Aircon.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.SampleData.Entity
{
    public static class SampleUserData
    {
        public static User GetUser()
        {
            return new User
            {
                Deleted = false,
                Disabled = false,
                Email = "john.doe1@example.com",
                FirstName = "System",
                LastName = "Admin",
                UserName = "john.doe1@example.com",
                EmailConfirmed = true,
                WorkTitle = "Supervisor",
                UserStatus = UserStatus.AwaitingReview
            };
        }
        public static User GetUserWithNoFirstName()
        {
            return new User {
                Deleted = false,
                Disabled = false,
                Email = "john.doe2@example.com",
                //FirstName = "System",
                LastName = "Admin",
                UserName = "john.doe2@example.com",
                EmailConfirmed = true,
                WorkTitle = "Supervisor",
                UserStatus = UserStatus.AwaitingReview
            };
        }
        public static User GetUserWithNoLastName()
        {
            return new User
            {
                Deleted = false,
                Disabled = false,
                Email = "john.doe3@example.com",
                FirstName = "System",
                //LastName = "Admin",
                UserName = "john.doe3@example.com",
                EmailConfirmed = true,
                WorkTitle = "Supervisor",
                UserStatus = UserStatus.AwaitingReview
            };
        }
        public static User GetUserWithNoWorkTitle()
        {
            return new User
            {
                Deleted = false,
                Disabled = false,
                Email = "john.doe4@example.com",
                FirstName = "System",
                LastName = "Admin",
                UserName = "john.doe4@example.com",
                EmailConfirmed = true,
                //WorkTitle = "Supervisor",
                UserStatus = UserStatus.AwaitingReview
            };
        }
    }
}
