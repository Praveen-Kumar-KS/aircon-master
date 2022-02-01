using Aircon.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Aircon.Business.Models.Shared;
using Aircon.Data.Enums;
using Aircon.Business.Models.Customer.Company;
using Aircon.Data.Entities;
using Aircon.Business.Media;

namespace Aircon.Business.Services.Customer
{
    public interface ICompanyService
    {
        CompanyProfileModel GetCompanyProfile(int custId);
        CompanyProfileModel SaveCompanyProfile(CompanyProfileModel companyModel);
        List<PaymentMethodModel> CardPaymentMethods(int custId);
        List<PaymentMethodModel> AchPaymentMethods(int custId);
        List<PaymentMethodModel> GetPaymentMethods(int custId);
        List<CustomerAddressModel> GetCustomerAddresses(int custId);
        CustomerAddressModel GetCustomerAddress(int addressId);
        PaymentMethodModel GetPaymentMethod(int paymentMethodId);
        CustomerAddressModel UpdateAddress(CustomerAddressModel updateCustomerAddressModel);
        CustomerAddressModel AddAddress(CustomerAddressModel model);
        PaymentMethodModel AddPaymentMethod(PaymentMethodModel addpayment);
        PaymentMethodModel UpdatePaymentMethod(PaymentMethodModel UpdatePaymentMethodModel);
        void DeleteAddress(int id);
    }
    public class CompanyService : ICompanyService
    {
        private readonly AirconDbContext _airconDbContext;
        public CompanyService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }
        public CompanyProfileModel GetCompanyProfile(int custId)
        {
            var result = _airconDbContext.Customers.Where(x => x.Id == custId).Include(x => x.MainAddress).Select(x =>
                 new CompanyProfileModel
                 {
                     Id = x.Id,
                     CompanyName = x.CompanyName,
                     AddressId = x.AddressId.HasValue ? x.AddressId.Value : 0,
                     Logo = new PictureModel { 
                        Id = x.LogoId,
                        EntityId = x.Id,
                        PictureType = PictureType.CompanyLogo
                     },
                     MainAddress = x.AddressId.HasValue ? new Models.Shared.AddressModel
                     {
                         Line1 = x.MainAddress.Line1,
                         Line2 = x.MainAddress.Line2,
                         City = x.MainAddress.City,
                         State = x.MainAddress.State,
                         Zip = x.MainAddress.Zip
                     } : new AddressModel()
                 }
            ).SingleOrDefault();
            if (result != null)
            {
                return result;
            }

            return new CompanyProfileModel();
        }
        public CompanyProfileModel SaveCompanyProfile(CompanyProfileModel companyProfileModel)
        {
            var customer = _airconDbContext.Customers.Where(x => x.Id == companyProfileModel.Id).Include(x => x.MainAddress).SingleOrDefault();
            customer.CompanyName = companyProfileModel.CompanyName;
            customer.MainAddress.Line1 = companyProfileModel.MainAddress.Line1;
            customer.MainAddress.Line2 = companyProfileModel.MainAddress.Line2;
            customer.MainAddress.City = companyProfileModel.MainAddress.City;
            customer.MainAddress.State = companyProfileModel.MainAddress.State;
            customer.MainAddress.Zip = companyProfileModel.MainAddress.Zip;
            customer.LogoId = companyProfileModel.Logo.Id == 0 ? null : companyProfileModel.Logo.Id;
            _airconDbContext.Customers.Update(customer);
            _airconDbContext.SaveChanges();
            return companyProfileModel;
        }
        public List<PaymentMethodModel> GetPaymentMethods(int custId, PaymentType paymentType)
        {
            var paymentMethods = _airconDbContext.CustomerPaymentMethods
                .Include(x=>x.PaymentMethod)
                .Where(x=> x.Id == custId)
                .Where(x => x.PaymentMethod.PaymentType == paymentType)
                .Select(x=> new PaymentMethodModel {
                    CustomerId = x.Id,
                    PaymentMethodId = x.PaymentMethodId,
                    PaymentType = x.PaymentMethod.PaymentType,
                    PaymentMethodDefault = x.PaymentMethod.PaymentMethodDefault,
                    CardNumber = x.PaymentMethod.CardNumber,
                    CardValidThrough = x.PaymentMethod.CardValidThrough,
                    CardCvv = x.PaymentMethod.CardCvv,
                    NameOnCard = x.PaymentMethod.NameOnCard,
                    BillingAddressId = x.PaymentMethod.BillingAddressId,
                    IsBillingAddressSameAsCompanyAddress = x.PaymentMethod.IsBillingAddressSameAsCompanyAddress,
                    CompanyName = x.PaymentMethod.CompanyName,
                    AccountNumber = x.PaymentMethod.AccountNumber,
                    Routing = x.PaymentMethod.Routing,
                    NameOnAccount = x.PaymentMethod.NameOnAccount,
                    AccountType = x.PaymentMethod.AccountType,
                    BillingAddress =  new AddressModel
                    {
                        Id = x.PaymentMethod.BillingAddress.Id,
                        NickName = x.PaymentMethod.BillingAddress.NickName,
                        Line1 = x.PaymentMethod.BillingAddress.Line1,
                        Line2 = x.PaymentMethod.BillingAddress.Line2,
                        City = x.PaymentMethod.BillingAddress.City,
                        State = x.PaymentMethod.BillingAddress.State,
                        Zip = x.PaymentMethod.BillingAddress.Zip,
                        IsActive = x.PaymentMethod.BillingAddress.IsActive
                    }

                }).ToList();
            return paymentMethods;
        }
        public List<PaymentMethodModel> CardPaymentMethods(int custId)
        {
            return GetPaymentMethods(custId, PaymentType.CreditCard);
        }

        public List<PaymentMethodModel> AchPaymentMethods(int custId)
        {
            return GetPaymentMethods(custId, PaymentType.ACH);
        }
        public List<PaymentMethodModel> GetPaymentMethods(int custId)
        {
            var paymentMethods = _airconDbContext.CustomerPaymentMethods
                .Include(x => x.PaymentMethod)
                .Where(x => x.Id == custId)
                .Select(x => new PaymentMethodModel
                {
                    CustomerId = x.Id,
                    PaymentMethodId = x.PaymentMethodId,
                    PaymentType = x.PaymentMethod.PaymentType,
                    PaymentMethodDefault = x.PaymentMethod.PaymentMethodDefault,
                    CardNumber = x.PaymentMethod.CardNumber,
                    CardValidThrough = x.PaymentMethod.CardValidThrough,
                    CardCvv = x.PaymentMethod.CardCvv,
                    NameOnCard = x.PaymentMethod.NameOnCard,
                    BillingAddressId = x.PaymentMethod.BillingAddressId,
                    IsBillingAddressSameAsCompanyAddress = x.PaymentMethod.IsBillingAddressSameAsCompanyAddress,
                    CompanyName = x.PaymentMethod.CompanyName,
                    AccountNumber = x.PaymentMethod.AccountNumber,
                    Routing = x.PaymentMethod.Routing,
                    NameOnAccount = x.PaymentMethod.NameOnAccount,
                    AccountType = x.PaymentMethod.AccountType,
                    BillingAddress = new AddressModel
                    {
                        Id = x.PaymentMethod.BillingAddress == null ? 0 : x.PaymentMethod.BillingAddress.Id,
                        NickName = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.NickName,
                        Line1 = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.Line1,
                        Line2 = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.Line2,
                        City = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.City,
                        State = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.State,
                        Zip = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.Zip,
                        IsActive = x.PaymentMethod.BillingAddress == null ? false : x.PaymentMethod.BillingAddress.IsActive
                    }
                }).ToList();
            return paymentMethods;
        }
        public PaymentMethodModel GetPaymentMethod(int paymentMethodId)
        {
            var paymentMethod = _airconDbContext.CustomerPaymentMethods
                .Include(x => x.PaymentMethod)
                .Where(x => x.PaymentMethodId == paymentMethodId)
                .Select(x => new PaymentMethodModel
                {
                    CustomerId = x.Id,
                    PaymentMethodId = x.PaymentMethodId,
                    PaymentType = x.PaymentMethod.PaymentType,
                    PaymentMethodDefault = x.PaymentMethod.PaymentMethodDefault,
                    CardNumber = x.PaymentMethod.CardNumber,
                    CardValidThrough = x.PaymentMethod.CardValidThrough,
                    CardCvv = x.PaymentMethod.CardCvv,
                    NameOnCard = x.PaymentMethod.NameOnCard,
                    BillingAddressId = x.PaymentMethod.BillingAddressId,
                    IsBillingAddressSameAsCompanyAddress = x.PaymentMethod.IsBillingAddressSameAsCompanyAddress,
                    CompanyName = x.PaymentMethod.CompanyName,
                    AccountNumber = x.PaymentMethod.AccountNumber,
                    Routing = x.PaymentMethod.Routing,
                    NameOnAccount = x.PaymentMethod.NameOnAccount,
                    AccountType = x.PaymentMethod.AccountType,
                    BillingAddress = new AddressModel
                    {
                        Id = x.PaymentMethod.BillingAddress == null ? 0 : x.PaymentMethod.BillingAddress.Id,
                        NickName = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.NickName,
                        Line1 = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.Line1,
                        Line2 = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.Line2,
                        City = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.City,
                        State = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.State,
                        Zip = x.PaymentMethod.BillingAddress == null ? null : x.PaymentMethod.BillingAddress.Zip,
                        IsActive = x.PaymentMethod.BillingAddress == null ? false : x.PaymentMethod.BillingAddress.IsActive
                    }
                }).SingleOrDefault();
            return paymentMethod;
        }
        public List<CustomerAddressModel> GetCustomerAddresses(int custId)
        {
            return _airconDbContext.CustomerAddresses.Where(x => x.Id == custId)
                .Include(x => x.Address)
                .Include(x => x.AddressType)
                .Select(x => new CustomerAddressModel
                {
                    CustomerId = x.Id,
                    AddressId = x.AddressId,
                    AddressTypeId = x.AddressTypeId,
                    AddressTypeName = x.AddressType.Name,
                    Address = new AddressModel
                    {
                        NickName = x.Address.NickName,
                        Line1 = x.Address.Line1,
                        Line2 = x.Address.Line2,
                        City = x.Address.City,
                        State = x.Address.State,
                        Zip = x.Address.Zip,
                        IsActive = x.Address.IsActive
                    }
                }).ToList();
        }
        public CustomerAddressModel GetCustomerAddress(int addressId)
        {
            return _airconDbContext.CustomerAddresses.Where(x => x.AddressId == addressId)
                .Include(x => x.Address)
                .Include(x => x.AddressType)
                .Select(x => new CustomerAddressModel
                {
                    CustomerId = x.Id,
                    AddressId = x.AddressId,
                    AddressTypeId = x.AddressTypeId,
                    AddressTypeName = x.AddressType.Name,
                    Address = new AddressModel
                    {
                        NickName = x.Address.NickName,
                        Line1 = x.Address.Line1,
                        Line2 = x.Address.Line2,
                        City = x.Address.City,
                        State = x.Address.State,
                        Zip = x.Address.Zip,
                        IsActive = x.Address.IsActive
                    }
                }).SingleOrDefault();
        }
        public PaymentMethodModel AddPaymentMethod(PaymentMethodModel addpayment)
        {
            CustomerPaymentMethod customerPaymentMethod = new CustomerPaymentMethod();
            customerPaymentMethod.Id = addpayment.CustomerId;
            customerPaymentMethod.PaymentMethod = new PaymentMethod();
            customerPaymentMethod.PaymentMethod.PaymentType = addpayment.PaymentType;
            customerPaymentMethod.PaymentMethod.PaymentMethodDefault = addpayment.PaymentMethodDefault;
            customerPaymentMethod.PaymentMethod.CardNumber = addpayment.CardNumber;
            customerPaymentMethod.PaymentMethod.CardValidThrough = addpayment.CardValidThrough;
            customerPaymentMethod.PaymentMethod.CardCvv = addpayment.CardCvv;
            customerPaymentMethod.PaymentMethod.NameOnCard = addpayment.NameOnCard;
            customerPaymentMethod.PaymentMethod.IsBillingAddressSameAsCompanyAddress = addpayment.IsBillingAddressSameAsCompanyAddress;
            customerPaymentMethod.PaymentMethod.CompanyName = addpayment.CompanyName;
            if (addpayment.PaymentType == PaymentType.CreditCard)
            {
                if (addpayment.IsBillingAddressSameAsCompanyAddress)
                {
                    var customer = _airconDbContext.Customers.Find(addpayment.CustomerId);
                    customerPaymentMethod.PaymentMethod.BillingAddressId = customer == null ? null : customer.AddressId;
                }
                else
                {
                    customerPaymentMethod.PaymentMethod.BillingAddress = new Address();
                    customerPaymentMethod.PaymentMethod.BillingAddress.NickName = addpayment.BillingAddress.NickName;
                    customerPaymentMethod.PaymentMethod.BillingAddress.Line1 = addpayment.BillingAddress.Line1;
                    customerPaymentMethod.PaymentMethod.BillingAddress.Line2 = addpayment.BillingAddress.Line2;
                    customerPaymentMethod.PaymentMethod.BillingAddress.City = addpayment.BillingAddress.City;
                    customerPaymentMethod.PaymentMethod.BillingAddress.State = addpayment.BillingAddress.State;
                    customerPaymentMethod.PaymentMethod.BillingAddress.Zip = addpayment.BillingAddress.Zip;
                    customerPaymentMethod.PaymentMethod.BillingAddress.IsActive = addpayment.BillingAddress.IsActive;
                }
            }
            else
            {
                customerPaymentMethod.PaymentMethod.AccountNumber = addpayment.AccountNumber;
                customerPaymentMethod.PaymentMethod.Routing = addpayment.Routing;
                customerPaymentMethod.PaymentMethod.NameOnAccount = addpayment.NameOnAccount;
                customerPaymentMethod.PaymentMethod.AccountType = addpayment.AccountType;
            }
            _airconDbContext.CustomerPaymentMethods.Add(customerPaymentMethod);
            _airconDbContext.SaveChanges();
            return addpayment;            
        }
        public PaymentMethodModel UpdatePaymentMethod(PaymentMethodModel UpdatePaymentMethodModel)
        {           
            PaymentMethod updatePayments = _airconDbContext.PaymentMethods.Find(UpdatePaymentMethodModel.PaymentMethodId);
            updatePayments.PaymentType = UpdatePaymentMethodModel.PaymentType;
            updatePayments.PaymentMethodDefault = UpdatePaymentMethodModel.PaymentMethodDefault;
            updatePayments.CardNumber = UpdatePaymentMethodModel.CardNumber;
            updatePayments.CardValidThrough = UpdatePaymentMethodModel.CardValidThrough;
            updatePayments.CardCvv = UpdatePaymentMethodModel.CardCvv;
            updatePayments.NameOnCard = UpdatePaymentMethodModel.NameOnCard;
            updatePayments.IsBillingAddressSameAsCompanyAddress = UpdatePaymentMethodModel.IsBillingAddressSameAsCompanyAddress;
            updatePayments.CompanyName = UpdatePaymentMethodModel.CompanyName;
            if (updatePayments.PaymentType == PaymentType.CreditCard)
            {
                if (updatePayments.IsBillingAddressSameAsCompanyAddress)
                {
                    var customer = _airconDbContext.Customers.Find(UpdatePaymentMethodModel.CustomerId);
                    updatePayments.BillingAddressId = customer == null ? null : customer.AddressId;
                }
                else
                {
                    Address updateaddress = _airconDbContext.Addresses.Find(UpdatePaymentMethodModel.BillingAddressId);
                    updateaddress.NickName = UpdatePaymentMethodModel.BillingAddress.NickName;
                    updateaddress.Line1 = UpdatePaymentMethodModel.BillingAddress.Line1;
                    updateaddress.Line2 = UpdatePaymentMethodModel.BillingAddress.Line2;
                    updateaddress.City = UpdatePaymentMethodModel.BillingAddress.City;
                    updateaddress.State = UpdatePaymentMethodModel.BillingAddress.State;
                    updateaddress.Zip = UpdatePaymentMethodModel.BillingAddress.Zip;
                    updateaddress.IsActive = UpdatePaymentMethodModel.BillingAddress.IsActive;
                    _airconDbContext.Addresses.Update(updateaddress);
                }
            }
            else
            {
                UpdatePaymentMethodModel.AccountNumber = updatePayments.AccountNumber;
                UpdatePaymentMethodModel.Routing = updatePayments.Routing;
                UpdatePaymentMethodModel.NameOnAccount = updatePayments.NameOnAccount;
                UpdatePaymentMethodModel.AccountType = updatePayments.AccountType;
            }
            _airconDbContext.PaymentMethods.Update(updatePayments);
            _airconDbContext.SaveChanges();
            return UpdatePaymentMethodModel;
        }

        public CustomerAddressModel AddAddress(CustomerAddressModel model)
       {

            CustomerAddress customerAddress = new CustomerAddress
            {
                Id = model.CustomerId,
                AddressTypeId = model.AddressTypeId,
                Address = new Address
                {
                    NickName = model.Address.NickName,
                    Line1 = model.Address.Line1,
                    Line2 = model.Address.Line2,
                    City = model.Address.City,
                    State = model.Address.State,
                    Zip = model.Address.Zip,
                    IsActive = true,
                }
               
            };
            _airconDbContext.CustomerAddresses.Add(customerAddress);
            _airconDbContext.SaveChanges();
            return model;
        }
        public CustomerAddressModel UpdateAddress(CustomerAddressModel updateAddressModel)
        {
          
            Address updateAddress = _airconDbContext.Addresses.Find(updateAddressModel.AddressId);
            updateAddress.NickName = updateAddressModel.Address.NickName;
            updateAddress.Line1 = updateAddressModel.Address.Line1;
            updateAddress.Line2 = updateAddressModel.Address.Line2;
            updateAddress.City = updateAddressModel.Address.City;
            updateAddress.State = updateAddressModel.Address.State;
            updateAddress.Zip = updateAddressModel.Address.Zip;
            updateAddress.IsActive = updateAddressModel.Address.IsActive;
            _airconDbContext.Addresses.Update(updateAddress);
            _airconDbContext.SaveChanges();
            return updateAddressModel;

        }
        public void DeleteAddress(int id)
        {
            Address address = _airconDbContext.Addresses.Find(id);
            _airconDbContext.Addresses.Remove(address);
            _airconDbContext.SaveChanges();
        }

    }
}
