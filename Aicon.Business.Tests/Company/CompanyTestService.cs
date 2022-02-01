using Aircon.Business.Seeder;
using Aircon.Business.Models.Shared;
using Aircon.Business.Services.Customer;
using Aircon.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Aircon.Business.Models.Customer.Company;
using Aircon.Data.Entities;
using Aircon.SampleData.Bogus;
using Aircon.Data.Enums;
using Aircon.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Aicon.Business.UnitTests.Company
{
    [Collection("AirconWebApplicationFactory")]
    public class CompanyTestService : IntegrationTestBase, IDisposable
    {
        private readonly ICompanyService _companyService;
        private Aircon.Data.Entities.Customer TestCustomer;

        public CompanyTestService(AirconWebApplicationFactory factory) : base(factory)
        {
            _companyService = GetRequiredService<ICompanyService>();
            TestCustomer = BogusCustomerData.GetCustomer(20).FirstOrDefault();
            AirconDbContext.Customers.Add(TestCustomer);
            AirconDbContext.SaveChanges();
        }
        [Fact]
        public void GetCompanyProfile ()
        {
            var result = _companyService.GetCompanyProfile(TestCustomer.Id);
            Assert.Equal(TestCustomer.Id, result.Id);
            Assert.Equal(TestCustomer.CompanyName, result.CompanyName);
        }
        [Fact]
        public void GetPaymentMethods_ReturnsRecords()
        {
            var result = _companyService.GetPaymentMethods(1);
            Assert.Equal(TestCustomer.PaymentMethods.Count, result.Count);
        }     
        [Fact]
        public void GetCustomerAddresses()
        {
            var result = _companyService.GetCustomerAddresses(TestCustomer.Id);
            Assert.Equal(TestCustomer.Id, result.Count);
        }

        [Fact]
        public void GetCustomerAddress()
        {
            var result = _companyService.GetCustomerAddress(TestCustomer.AddressId.Value);
            Assert.Equal(TestCustomer.AddressId, result.AddressId);
        }

        [Fact]
        public void GetPaymentMethods()
        {
            var result = _companyService.GetPaymentMethods(TestCustomer.Id);
            Assert.Equal(TestCustomer.PaymentMethods.Count, result.Count);
        }

       [Fact]
        public void CardPaymentMethods()
        {
            var result = _companyService.CardPaymentMethods(TestCustomer.Id);
            Assert.Equal(TestCustomer.PaymentMethods.Count, result.Count);
        }
        [Fact]
        public void AchPaymentMethods()
        {
            var result = _companyService.AchPaymentMethods(TestCustomer.Id);
            Assert.Equal(TestCustomer.PaymentMethods.Count, result.Count);
        }

        [Fact]
        public void SaveCompanyProfile()
        {
            var save = new CompanyProfileModel();
            var savecompany = _companyService.GetCompanyProfile(2);
            savecompany.CompanyName = "VG";
            savecompany.MainAddress.NickName = "Admin";
            savecompany.MainAddress.Line1 = "12";
            savecompany.MainAddress.Line2 = "John street";
            savecompany.MainAddress.City = "Los";
            savecompany.MainAddress.State = "North America";
            savecompany.MainAddress.Zip = "97537";
            var saveresult = _companyService.SaveCompanyProfile(savecompany);
            Assert.Equal(save.Id, saveresult.Id);
            Assert.Equal(savecompany.CompanyName, saveresult.CompanyName);
            Assert.Equal(savecompany.MainAddress.NickName, saveresult.MainAddress.NickName);
            Assert.Equal(savecompany.MainAddress.Line1, saveresult.MainAddress.Line1);
            Assert.Equal(savecompany.MainAddress.Line2, saveresult.MainAddress.Line2);
            Assert.Equal(savecompany.MainAddress.City, saveresult.MainAddress.City);
            Assert.Equal(savecompany.MainAddress.Zip, saveresult.MainAddress.Zip);
        }

        [Fact]
        public void AddPayment()
        {
            var request = new PaymentMethodModel();
            var test = new CustomerPaymentMethod();
            test.Id = 1;
            request.CustomerId = test.Id;
            request.PaymentMethodId = 1;
            request.PaymentType = PaymentType.CreditCard;
            request.PaymentMethodDefault = PaymentMethodDefault.Shipping;
            request.CardNumber = "7356983720982347";
            request.CardValidThrough = "12/21";
            request.CardCvv = "233";
            request.NameOnCard = "John Snow";
            request.IsBillingAddressSameAsCompanyAddress = true;
            request.CompanyName = "VG";
            var result = _companyService.AddPaymentMethod(request);
            Assert.NotEqual(test.Id, result.CustomerId);
        }

        [Fact]
        public void UpdatePayment()
        {
            var request = _companyService.GetPaymentMethod(TestCustomer.Id);
            request.PaymentType = PaymentType.CreditCard;
            request.PaymentMethodDefault = PaymentMethodDefault.Shipping;
            request.CardNumber = "5431879187621658";
            request.CardValidThrough = "12/21";
            request.NameOnCard = "John Snow";
            request.IsBillingAddressSameAsCompanyAddress = true;
            request.CompanyName = "VG";
            var result = _companyService.UpdatePaymentMethod(request);
            Assert.Equal(request.PaymentMethodId, result.PaymentMethodId);
            Assert.Equal(request.PaymentType, result.PaymentType);
            Assert.Equal(request.PaymentMethodDefault, result.PaymentMethodDefault);
            Assert.Equal(request.CardNumber, result.CardNumber);
            Assert.Equal(request.CardValidThrough, result.CardValidThrough);
            Assert.Equal(request.IsBillingAddressSameAsCompanyAddress, result.IsBillingAddressSameAsCompanyAddress);
            Assert.Equal(request.CompanyName, result.CompanyName);
        }

        [Fact]
        public void AddCompanyAddress()
        {
            var request = new CustomerAddressModel();
            request.Address.Line1 = "123 Building";
            request.Address.Line2 = "John street";
            request.Address.City = "Los";
            request.Address.State = "Los Angels";
            request.Address.Zip = "87547";
            request.Address.IsActive = true;
            var result = _companyService.AddAddress(request);
            Assert.NotEqual(0,result.AddressTypeId);
            Assert.NotEqual(2, result.CustomerId);
        }

        [Fact]
        public void UpdateCompanyAddress()
        {
            var request = new CustomerAddressModel();
            request.Address.Id = 1;
            request.Address.NickName = "Admin";
            request.Address.Line1 = "123 Building";
            request.Address.Line2 = "John street";
            request.Address.City = "Los";
            request.Address.State = "Los Angels";
            request.Address.Zip = "87547";
            request.Address.IsActive = true;
            var result = _companyService.UpdateAddress(request);
            Assert.Equal(request.AddressId, result.AddressId);
            Assert.Equal(request.Address.NickName, result.Address.NickName);
            Assert.Equal(request.Address.Line1, result.Address.Line2);
            Assert.Equal(request.Address.Line2, result.Address.Line2);
            Assert.Equal(request.Address.City, result.Address.City);
            Assert.Equal(request.Address.State, result.Address.State);
            Assert.Equal(request.Address.Zip, result.Address.Zip);
            Assert.Equal(request.Address.IsActive, result.Address.IsActive);
        }

        [Fact]
        public void DeleteCompanyAddress()
        {
            var Id = TestCustomer.Id;
            _companyService.DeleteAddress(Id);
            var data = _companyService.GetCustomerAddress(Id);
            Assert.IsType<NotFoundResult>(data);
        }


        public void Dispose()
        {
            AirconDbContext.Customers.Remove(TestCustomer);
        }
    }
}

