using Aircon.Data.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.SampleData.Bogus
{
    public class CustomerData
    {
        public static Faker<Customer> Customers { get; } =
            new Faker<Customer>()
                .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
                .RuleFor(x => x.DisplayCustomerId, f => f.Company.Random.Number().ToString())
                .RuleFor(x => x.FranchiseParent, f => f.Company.CompanyName().ToString())
                .RuleFor(x => x.AdminEmail, f => f.Internet.Email())
                .RuleFor(x => x.AlternateEmail, f => f.Internet.Email())
                .RuleFor(x => x.IATANumber, f => f.Address.CountryCode())
                .RuleFor(x => x.EinOrSsn, f => f.Person.Random.Number().ToString()) //"AAA-GG-SSSS"
            .RuleFor(x => x.IsTermsAccepted, f => f.Random.Bool(80))
            .RuleFor(x => x.IsSetupCompleted, f => f.Random.Bool(80))
            .RuleFor(x => x.IsPaymentProcessed, f => f.Random.Bool(80))
            .RuleFor(x => x.IsSubscriptionExpired, f => f.Random.Bool(20))
            .RuleFor(x => x.SubscriptionExpiryDateUtc, f => f.Date.Future(1))
            .RuleFor(x => x.MainAddress.Line1, f => f.Address.StreetAddress())
            .RuleFor(x => x.MainAddress.Line2, f => f.Address.SecondaryAddress())
            .RuleFor(x => x.MainAddress.City, f => f.Address.City())
            .RuleFor(x => x.MainAddress.State, f => f.Address.State())
            .RuleFor(x => x.MainAddress.Zip, f => f.Address.ZipCode())
            .RuleFor(x => x.MainAddress.SpecialInstruction, f => f.Lorem.Lines(1));

    }
}
