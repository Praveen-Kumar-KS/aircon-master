using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Aircon.Data.Security;
using AutoBogus;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.SampleData.Bogus
{
    // First Step Customer Users Screen
    public static class BogusEmployeeData
    {
        public static Faker<FakeUser> User { get; } =
         new Faker<FakeUser>()
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName)
            .RuleFor(x => x.WorkTitle, f => f.Random.ArrayElement<string>(UserTitles().ToArray()))
            .RuleFor(x => x.CreationDate, f => f.Date.Past(1))
            .RuleFor(x => x.ApprovedDate, f => f.Date.Past(1))
            .RuleFor(x => x.ActivatedDate, f => f.Date.Past(1))
            .RuleFor(x => x.SignedUpDate, f => f.Date.Past(1))
            .RuleFor(x=> x.IsEmployee, f=> true)
            .RuleFor(x => x.IsActive, f => f.Random.Bool(0.7f))
            .RuleFor(x => x.IsApproved, f => f.Random.Bool(80))
            .RuleFor(x => x.IsEmployee, f => f.Random.Bool(10))
            .RuleFor(u => u.DisplayUserId, f => f.Random.Replace("#########"))
            .RuleFor(x => x.PhoneNumber, f => f.Person.Phone)
            .RuleFor(x => x.Email, (f, x) => f.Internet.Email(firstName: f.Person.FirstName, lastName: string.Empty, provider: "aircon.com"))//  f.Internet.DomainName()  )
            .RuleFor(x => x.Role, f => f.PickRandom(RoleSystemName.Administrators, RoleSystemName.SystemAdministrators, RoleSystemName.WarehouseAssociate))
            .RuleFor(x => x.UserStatus, f => f.PickRandom(UserStatus.Approved, UserStatus.AwaitingReview, UserStatus.Denied))
             ;
        public static List<string> UserTitles()
        {
            return new List<string> { "Supervisor", "Director", "Administrator", "Manager", "Warehouse Keeper" };
        }

        public static List<FakeUser> GetUsers()
        {
            return User.Generate(50);
        }

        public static void Customer()
        {


        }
    }

}
