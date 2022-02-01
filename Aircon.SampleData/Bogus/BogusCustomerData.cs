using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Aircon.Data.Security;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.SampleData.Bogus
{

    public static class BogusCustomerData
    {
        public static Faker<Customer> Customer { get; } =
            new Faker<Customer>()
            .RuleFor(x => x.CustomerDomains, (f, x) => new List<CustomerDomain> { new CustomerDomain { DomainName = f.Internet.UserName(x.CompanyName, string.Empty) + "." + f.Internet.DomainSuffix() } })
            .RuleFor(x => x.DisplayCustomerId, f => f.Random.Replace("#########"))
            .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
            .RuleFor(x => x.FranchiseParent, f => f.Company.CompanyName())
            .RuleFor(x => x.AdminEmail, (f, x) => f.Internet.Email(firstName: f.Person.FirstName, lastName: string.Empty, provider: x.CustomerDomains.FirstOrDefault().DomainName))//  f.Internet.DomainName()  )
            .RuleFor(x => x.AdminName, f => string.Format("{0} {1}", f.Person.FirstName, f.Person.LastName))
            .RuleFor(x => x.AdminPhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
            .RuleFor(x => x.AlternateEmail, f => f.Internet.Email())
            .RuleFor(x => x.IATANumber, f => f.Random.ArrayElement<string>(IATA().ToArray()))
            .RuleFor(x => x.SubscriptionId, f => 1)
            .RuleFor(x => x.CreationDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.EinOrSsn, f => f.Random.Replace("###-##-####"))
            .RuleFor(x => x.ActivatedDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.ApprovedDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.SignedUpDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.IsPaymentProcessed, f => f.Random.Bool(1))
            .RuleFor(x => x.IsSetupCompleted, f => f.Random.Bool(1))
            .RuleFor(x => x.IsActive, f => f.Random.Bool(0.8f))
            .RuleFor(x => x.IsSubscriptionExpired, f => f.Random.Bool(0.9f))
            .RuleFor(x => x.NoOfBranches, f => f.PickRandom(NoOfBranches.OneToFive, NoOfBranches.SixToTen, NoOfBranches.ElevenToTwenty, NoOfBranches.AboveTwenty))
            .RuleFor(x => x.MainAddress, f => GetAddress())
            .RuleFor(x => x.CustomerAddresses, f => GetCustomerAddress())
            .RuleFor(x => x.PaymentMethods, f => GetCustomerPaymentMethod())
            .RuleFor(x => x.CustomerContacts, f => GetCustomerContacts())
            .RuleFor(x => x.Users, f => GetUsers())
            .RuleFor(x=> x.CustomerNotes, f=> GetCustomerNotes())
             ;

        public static List<string> IATA()
        {
            return new List<string> { "DAL", "DFW", "ELP", "HOU", "IAH", "CRP" };
        }

        public static List<Customer> GetCustomer(int number = 10)
        {
            return Customer.Generate(number);
        }

        public static Faker<CustomerOpportunity> CustomerOpportunity { get; } =
            new Faker<CustomerOpportunity>()
            .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
            .RuleFor(x => x.FranchiseParent, f => f.Company.CompanyName())
            .RuleFor(x => x.AdminEmail, f => f.Internet.Email())
            .RuleFor(x => x.AdminName, f => string.Format("{0} {1}", f.Person.FirstName, f.Person.LastName))
            .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
            .RuleFor(x => x.AlternateEmail, f => f.Internet.Email())
            .RuleFor(x => x.IATANumber, f => f.Random.ArrayElement<string>(IATA().ToArray()))
            .RuleFor(x => x.SubscriptionId, f => 1)
            .RuleFor(x => x.AbandonedDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.CallbackScheduledDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.ApprovedDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.SignedUpDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.IsPaymentProcessed, f => f.Random.Bool(1))
            .RuleFor(x => x.IsSetupCompleted, f => f.Random.Bool(1))
            .RuleFor(x => x.NoOfBranches, f => f.PickRandom(NoOfBranches.OneToFive, NoOfBranches.SixToTen, NoOfBranches.ElevenToTwenty, NoOfBranches.AboveTwenty))
            .RuleFor(x => x.InterestLevel, f => f.PickRandom(InterestLevel.High, InterestLevel.Medium, InterestLevel.Low))
            .RuleFor(x => x.Status, f => f.PickRandom(CustomerOpportunityStatus.CallbackScheduled, CustomerOpportunityStatus.Abandoned, CustomerOpportunityStatus.Approved))
            .RuleFor(x => x.MainAddress, f => GetAddress())
            .RuleFor(x => x.CustomerOpportunityAddresses, f => GetCustomerOpportunityAddress())
            .RuleFor(x => x.PaymentMethods, f => GetCustomerOpportunityPaymentMethod())
            .RuleFor(x => x.Users, f => GetUsers(1, 1))
             ;

        public static Faker<Address> SingleAddress { get; } =
            new Faker<Address>()
            .RuleFor(x => x.NickName, f => f.Address.BuildingNumber())
            .RuleFor(x => x.Line1, f => f.Address.StreetAddress())
            .RuleFor(x => x.Line2, f => f.Address.SecondaryAddress())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.State, f => f.Address.State())
            .RuleFor(x => x.Zip, f => f.Address.ZipCode())
             ;

        public static List<CustomerOpportunity> GetCustomerOpportunity(int number=10)
        {
            return CustomerOpportunity.Generate(number);
        }

        public static Address GetAddress()
        {
            return SingleAddress.Generate();
        }


        public static Faker<CustomerAddress> CustomerAddress { get; } =
            new Faker<CustomerAddress>()
            .RuleFor(x => x.Address, f => GetAddress())
            .RuleFor(x => x.AddressTypeId, f => f.Random.Int(1, 2))
             ;

        public static List<CustomerAddress> GetCustomerAddress()
        {
            return CustomerAddress.Generate(new Faker().Random.Number(0, 10));
        }

        public static Faker<PaymentMethod> PaymentMethod { get; } =
            new Faker<PaymentMethod>()
            .RuleFor(x => x.PaymentType, f => f.PickRandom(PaymentType.CreditCard, PaymentType.CreditCard))
            .RuleFor(x => x.PaymentMethodDefault, f => f.PickRandom(PaymentMethodDefault.Shipping, PaymentMethodDefault.Subscription, PaymentMethodDefault.SubscriptionAndShipping))
            .RuleFor(x => x.CardNumber, f => f.Finance.CreditCardNumber())
            .RuleFor(x => x.CardValidThrough, f => f.PickRandom("02/2024", "05/2022", "05/2027", "03/2022"))
            .RuleFor(x => x.CardCvv, f => f.Finance.CreditCardCvv())
            .RuleFor(x => x.NameOnCard, f => f.Person.FullName)
            .RuleFor(x => x.IsBillingAddressSameAsCompanyAddress, f => f.Random.Bool(0.4f))
            .RuleFor(x => x.BillingAddress, f => GetAddress())
            .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
            .RuleFor(x => x.AccountNumber, f => f.Finance.Account())
            .RuleFor(x => x.Routing, f => f.Finance.RoutingNumber())
            .RuleFor(x => x.NameOnAccount, f => f.Person.FirstName)
            .RuleFor(x => x.AccountType, f => f.PickRandom(AccountType.Checking, AccountType.Savings))
             ;


        public static Faker<NotificationSetting> NotificationSettings { get; } =
            new Faker<NotificationSetting>()
            .RuleFor(x => x.Id, f => f.Random.Int(1, 17))
            .RuleFor(x => x.Name, f => f.Random.String())
            .RuleFor(x => x.SystemName, f => f.Random.String())
            .RuleFor(x => x.NotificationGroup, f => f.PickRandom(NotificationGroup.BookingNeedsAttention, NotificationGroup.QuotesExpiring, NotificationGroup.ShipmentDashboardNotification, NotificationGroup.ShipmentDelayed, NotificationGroup.ShipmentNeedsAttention))
            .RuleFor(x => x.TemplateDefinition, f => GetTemplateDefinition())
             ;

        public static List<NotificationSetting> GetNotificationSetting()
        {
            return NotificationSettings.Generate(new Faker().Random.Number(1, 15));
        }

        public static TemplateDefinition GetTemplateDefinition()
        {
            return SingleTemplateDefinition.Generate();
        }

        public static Faker<TemplateDefinition> SingleTemplateDefinition { get; } =
           new Faker<TemplateDefinition>()
           .RuleFor(x => x.Id, f => f.Random.Int(1, 5))
           .RuleFor(x => x.Instructions, f => f.Random.String())
           .RuleFor(x => x.IsLayout, f => f.Random.Bool())
           .RuleFor(x => x.Layout, f => f.Random.String())
           .RuleFor(x => x.MaxNameLength, f => f.Random.Int())
           .RuleFor(x => x.Name, f => f.Random.String())
           .RuleFor(x => x.SampleTemplateText, f => f.Random.String())
           .RuleFor(x => x.TemplateText, f => f.Random.String())
           .RuleFor(x => x.EmailSubjectText, f => f.Random.String())
            ;


        public static Faker<Preference> Preference { get; } =
             new Faker<Preference>()
              .RuleFor(x => x.Id, f => f.Random.Int(1, 6))
              .RuleFor(x => x.LandingPage, f => f.PickRandom(LandingPage.Metric, LandingPage.Workflow))
              .RuleFor(x => x.MeasurementUnit, f => f.PickRandom(MeasurementUnit.Imperial, MeasurementUnit.Metric))
              .RuleFor(x => x.CountryId, f => f.Random.Int(1, 6))
              .RuleFor(x => x.WindowsTimeZoneId, f => f.Random.Int(1, 6))
              .RuleFor(x => x.Country, f => GetCountry())
              .RuleFor(x => x.WindowsTimeZone, f => GetWindowsTimeZone())
               ;

        public static List<Preference> GetPreference()
        {
            return Preference.Generate(new Faker().Random.Number(1, 10));
        }

        public static Country GetCountry()
        {
            return SingleCountry.Generate();
        }

        public static WindowsTimeZone GetWindowsTimeZone()
        {
            return SingleWindowsTimeZone.Generate();
        }

        public static Faker<Country> SingleCountry { get; } =
           new Faker<Country>()
           .RuleFor(x => x.CountryName, f => f.Address.Country())
           .RuleFor(x => x.IsoAlpha3, f => f.Address.CountryCode())
           .RuleFor(x => x.IsoAlpha2, f => f.Address.CountryCode())
            ;

        public static Faker<WindowsTimeZone> SingleWindowsTimeZone { get; } =
           new Faker<WindowsTimeZone>()
           .RuleFor(x => x.Name, f => f.Date.TimeZoneString())
           .RuleFor(x => x.Active, f => f.Random.Bool())
            ;
       

        public static PaymentMethod GetPaymentMethod()
        {
            return PaymentMethod.Generate();
        }

        public static Faker<CustomerPaymentMethod> CustomerPaymentMethod { get; } =
            new Faker<CustomerPaymentMethod>()
            .RuleFor(x => x.PaymentMethod, f => GetPaymentMethod())
             ;

        public static List<CustomerPaymentMethod> GetCustomerPaymentMethod()
        {
            return CustomerPaymentMethod.Generate(new Faker().Random.Number(0, 5));
        }

        public static Faker<CustomerOpportunityAddress> CustomerOpportunityAddress { get; } =
            new Faker<CustomerOpportunityAddress>()
            .RuleFor(x => x.Address, f => GetAddress())
            .RuleFor(x => x.AddressTypeId, f => f.Random.Int(1, 2))
             ;

        public static List<CustomerOpportunityAddress> GetCustomerOpportunityAddress()
        {
            return CustomerOpportunityAddress.Generate(5);
        }

        public static Faker<CustomerOpportunityPaymentMethod> CustomerOpportunityPaymentMethod { get; } =
            new Faker<CustomerOpportunityPaymentMethod>()
            .RuleFor(x => x.PaymentMethod, f => GetPaymentMethod())
             ;

        public static List<CustomerOpportunityPaymentMethod> GetCustomerOpportunityPaymentMethod()
        {
            return CustomerOpportunityPaymentMethod.Generate(4);
        }

        public static Faker<Contact> Contact { get; } =
            new Faker<Contact>()
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName)
            .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
            .RuleFor(x => x.Title, f => f.PickRandom<string>(Titles().ToArray()))
            .RuleFor(x => x.Department, f => f.Commerce.Department())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.Email, (f, x) => f.Internet.Email(firstName: f.Person.FirstName, lastName: string.Empty, provider: f.Internet.UserName(x.CompanyName, string.Empty) + "." + f.Internet.DomainSuffix()))//  f.Internet.DomainName()  )
            .RuleFor(x => x.Active, f => f.Random.Bool(0.9f))
            .RuleFor(x => x.SpecialInstruction, f => f.Lorem.Sentence())
             ;
        public static List<string> Titles()
        {
            return new List<string> { "Supervisor", "Director", "Administrator", "Manager", "Clerk" };
        }

        public static Contact GetSingleContact()
        {
            return Contact.Generate();
        }

        public static Faker<CustomerContact> CustomerContact { get; } =
            new Faker<CustomerContact>()
            .RuleFor(x => x.Address, f => GetAddress())
            .RuleFor(x => x.Contact, f => GetSingleContact())
             ;

        public static List<CustomerContact> GetCustomerContacts()
        {
            return CustomerContact.Generate(15);
        }

        public static Faker<User> User { get; } =
         new Faker<User>()
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName)
            .RuleFor(x => x.WorkTitle, f => f.Random.ArrayElement<string>(UserTitles().ToArray()))
            .RuleFor(x => x.CreationDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.ApprovedDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.ActivatedDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.SignedUpDateUtc, f => f.Date.Past(1))
            .RuleFor(x => x.IsActive, f => f.Random.Bool(50))
            .RuleFor(x => x.IsApproved, f => f.Random.Bool(50))
            .RuleFor(x => x.IsEmployee, f => false)
            .RuleFor(u => u.DisplayUserId, f => f.Random.Replace("#########"))
            .RuleFor(x => x.PhoneNumber, f => f.Person.Phone)
            .RuleFor(x => x.Email, (f, x) => f.Internet.Email(firstName: x.FirstName, lastName: x.LastName, provider: string.Empty))
            .RuleFor(x => x.UserStatus, f => f.PickRandom(UserStatus.Approved, UserStatus.AwaitingReview, UserStatus.Denied))
            //.RuleFor(x=> x.UserNotes, f=> GetUserNotes())
             ;
        public static List<string> UserTitles()
        {
            return new List<string> { "Supervisor", "Director", "Administrator", "Manager", "Warehouse Keeper" };
        }

        public static List<User> GetUsers(int min = 5, int max = 25)
        {
            return User.Generate(new Faker().Random.Number(min, max));
        }

        public static string GetRole()
        {
            return new Faker().PickRandom(RoleSystemName.Administrators, RoleSystemName.Manager, RoleSystemName.Sales, RoleSystemName.User, RoleSystemName.SystemAdministrators, RoleSystemName.WarehouseAssociate);
        }

        public static string GetDomain()
        {
            Faker f = new Faker();
            return new Faker().PickRandom(f.Internet.UserName(f.Company.CompanyName(), string.Empty) + "." + f.Internet.DomainSuffix());
        }



        public static Note GetSingleNote()
        {
            return Note.Generate();
        }

        public static Faker<Note> Note { get; } =
            new Faker<Note>()
            .RuleFor(x => x.Text, f => f.Lorem.Text())
            .RuleFor(x => x.CreatedOnUtc, f => f.Date.Past(1/12))
            .RuleFor(x => x.CreatedById, 1);

        public static Faker<CustomerNote> CustomerNote { get; } =
            new Faker<CustomerNote>()
           .RuleFor(x => x.Note, f => GetSingleNote())
             ;

        public static List<CustomerNote> GetCustomerNotes()
        {
            return CustomerNote.Generate(15);
        }

        public static Faker<UserNote> UserNote { get; } =
            new Faker<UserNote>()
           .RuleFor(x => x.Note, f => GetSingleNote())
             ;

        public static List<UserNote> GetUserNotes()
        {
            return UserNote.Generate(5);
        }
    }

}
