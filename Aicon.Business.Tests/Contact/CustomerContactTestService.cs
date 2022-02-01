using Aircon.Business.Models.Customer.Contact;
using Aircon.Business.Seeder;
using Aircon.Business.Services.Customer;
using Aircon.Test;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aicon.Business.UnitTests.Contact
{
    [Collection("AirconWebApplicationFactory")]
    public class CustomerContactTestService : IntegrationTestBase, IDisposable
    {

        private readonly ICustomerContactService _customercontactService;
        private readonly ITestDataContributor _testDataContributor;

        public CustomerContactTestService(AirconWebApplicationFactory factory) : base(factory)
        {
            _customercontactService = GetRequiredService<ICustomerContactService>();
        }

        [Fact]
        public void GetCustomerContacts()
        {           
            var request = new CustomerContactModel();
            var result = _customercontactService.GetCustomerContacts(1, string.Empty);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void GetCustomerContact()
        {
            var contact = _customercontactService.GetCustomerContact(1);
            Assert.Equal(1, contact.ContactId);
        }

        [Fact]
        public void AddContact()
        {
            var contactrequest = new CustomerContactModel();
            //var addressrequest = new CustomerContactModel();
            contactrequest.Contact.FirstName = "Albus";
            contactrequest.Contact.LastName = "Dumbelldore";
            contactrequest.Contact.Title = "SDE";
            contactrequest.Contact.Email = "sysadmin@valueglobal.net";
            contactrequest.Contact.PhoneNumber = "9671269807";
            contactrequest.Contact.CompanyName = "VGIT";
            contactrequest.Contact.SpecialInstruction = "I'm Software Engineer";
            contactrequest.Contact.Department = "Admin";
            contactrequest.Address.NickName = "Dumbelldore";
            contactrequest.Address.Line1 = " No. 198, Second Street";
            contactrequest.Address.Line2 = "main road, Green valley";
            contactrequest.Address.City = "Los Angel";
            contactrequest.Address.State = "New York";
            contactrequest.Address.Zip = "123456";
            contactrequest.Address.IsActive = true;
            var contactresult = _customercontactService.AddContact(contactrequest);
            //var addressresult = _customercontactService.AddContact(addressrequest);
            Assert.NotEqual(0,contactresult.AddressId);
            Assert.NotEqual(1, contactresult.ContactId);
            //Assert.NotEqual(1, addressresult.ContactId);
        }

        [Fact]
        public void UpdateContact()
        {
            var contactrequest = new CustomerContactModel();
            var addressrequest = new CustomerContactModel();
            contactrequest.ContactId = 1;
            addressrequest.AddressId = 1;
            contactrequest.Contact.FirstName = "Steve";
            contactrequest.Contact.LastName = "Roggers";
            contactrequest.Contact.Title = "SDE2";
            contactrequest.Contact.Email = "sysadmin098@valueglobal.net";
            contactrequest.Contact.PhoneNumber = "9675439807";
            contactrequest.Contact.CompanyName = "VG";
            contactrequest.Contact.Department = "Engineer";
            addressrequest.Address.NickName = "Steve";
            addressrequest.Address.Line1 = " No. 198, Third Street";
            addressrequest.Address.Line2 = "main road, Green wall";
            addressrequest.Address.City = "Queens";
            addressrequest.Address.State = "New York City";
            addressrequest.Address.Zip = "123098";
            var contactresult = _customercontactService.UpdateContact(contactrequest);
            var addressresult = _customercontactService.UpdateContact(addressrequest);
            Assert.Equal(1, contactresult.ContactId);
            Assert.Equal(1, contactresult.AddressId);
            Assert.Equal("Steve", contactresult.Contact.FirstName);
            Assert.Equal("Roggers", contactresult.Contact.LastName);
            Assert.Equal("SDE2", contactresult.Contact.Title);
            Assert.Equal("sysadmin098@valueglobal.net", contactresult.Contact.Title);
            Assert.Equal("9675439807", contactresult.Contact.Email);
            Assert.Equal("VG", contactresult.Contact.PhoneNumber);
            Assert.Equal("Software Developer", contactresult.Contact.CompanyName);
            Assert.Equal("Steve", contactresult.Contact.Department);
            Assert.Equal("No. 198, Third Street", addressresult.Address.NickName);
            Assert.Equal("main road, Green wall", addressresult.Address.Line1);
            Assert.Equal("Queens", addressresult.Address.City);
            Assert.Equal("New York City", addressresult.Address.State);
            Assert.Equal("123098", addressresult.Address.Zip);
        }

        [Fact]
        public void DeleteContact()
        {
            var Id = 5;
            _customercontactService.DeleteContact(Id);
            var data = _customercontactService.GetCustomerContact(Id);
            Assert.IsType<NotFoundResult>(data);
        }
       
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }

    
}
