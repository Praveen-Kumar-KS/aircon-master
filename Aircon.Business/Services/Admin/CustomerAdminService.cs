using Aircon.Business.Models.Admin.Customer;
using Aircon.Business.Models.Shared;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Aircon.Business.Services.Admin
{
    public interface ICustomerAdminService
    {
        List<CustomerOpportunityAdminModel> GetCustomerOpportunities(CustomerOpportunityStatus status,string searchText, int recordCountCustomersOpportunityQueue);
        List<CustomerAdminModel> GetCustomers( bool isActive, bool isAll, string searchText, int recordCountCustomersProfileQueue);
       // CustomerAdminModel GetCustomerById(int id);
        CustomerAdminModel UpdateCustomer(CustomerAdminModel updateCustomerAdminModel);
        CustomerOpportunityAdminModel UpdateCustomerOpportunity(CustomerOpportunityAdminModel updateCustomerOppAdminModel);
        void DeleteCustomerProfile(int id);
        void DeleteCustomerOpportunity(int id);
        void DeactiveCustomer(int id);
        void ActivateCustomer(int id);
        void DenyCustomerOpportunity(int id);
        void ApproveCustomer(int id);
        CustomerAdminModel GetCustomer(int id);
        CustomerOpportunityAdminModel GetCustomerOpportunity(int id);

    }
    public class CustomerAdminService : ICustomerAdminService
    {
        private readonly AirconDbContext _airconDbContext;
        public CustomerAdminService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }
       

        public List<CustomerOpportunityAdminModel> GetCustomerOpportunities(CustomerOpportunityStatus status, string searchText, int recordCountCustomersOpportunityQueue)
        {
            var customerOpportunities = _airconDbContext.CustomerOpportunities.Where(x => x.Status == status)
                .Include(x => x.MainAddress)
                 .Select(x => new CustomerOpportunityAdminModel
                 {
                     CustomerOpportunityId = x.Id,
                     CompanyName = x.CompanyName,
                     AdminEmail = x.AdminEmail,
                     AdminName = x.AdminName,
                     AlternateEmail = x.AlternateEmail,
                     IATANumber = x.IATANumber,
                     AdminPhoneNumber = x.AdminPhoneNumber,
                     FranchiseParent = x.FranchiseParent,
                     EinOrSsn = x.EinOrSsn,
                     SubscriptionId = x.SubscriptionId,
                     IsPaymentProcessed = x.IsPaymentProcessed,
                     IsTermsAccepted = x.IsTermsAccepted,
                     IsSetupCompleted = x.IsSetupCompleted,
                     SignedUpDateUtc = x.SignedUpDateUtc,
                     ApprovedDateUtc = x.ApprovedDateUtc,
                     AbandonedDateUtc = x.AbandonedDateUtc,
                     CallbackScheduledDateUtc = x.CallbackScheduledDateUtc,
                     InterestLevel = x.InterestLevel,
                     Status = x.Status,
                     AddressId = x.AddressId,
                     NoOfBranches = x.NoOfBranches,
                     Id = x.Id,
                     MainAddress = x.AddressId.HasValue ? new AddressModel
                     {
                         Id = x.MainAddress.Id,
                         NickName = x.MainAddress.NickName,
                         Line1 = x.MainAddress.Line1,
                         Line2 = x.MainAddress.Line2,
                         City = x.MainAddress.City,
                         State = x.MainAddress.State,
                         Zip = x.MainAddress.Zip,
                         IsActive = x.MainAddress.IsActive
                     } : new AddressModel()

                 });

            if (searchText != null)
            {
                customerOpportunities =
                     customerOpportunities.Where(x =>
                          (x.CompanyName == null ? false : x.CompanyName.ToUpper().Contains(searchText.ToUpper())) ||
                          (x.AdminName == null ? false : x.AdminName.ToUpper().Contains(searchText.ToUpper())) ||
                           (x.AdminEmail == null ? false : x.AdminEmail.ToUpper().Contains(searchText.ToUpper())) ||
                          (x.AdminPhoneNumber == null ? false : x.AdminPhoneNumber.Contains(searchText)) ||
                          (x.EinOrSsn == null ? false : x.EinOrSsn.ToUpper().Contains(searchText.ToUpper()))
                         ).Select(y => y);
            }
            customerOpportunities = customerOpportunities.Take(recordCountCustomersOpportunityQueue);
            return customerOpportunities.ToList();
        }

        public CustomerOpportunityAdminModel GetCustomerOpportunity(int id)
        {
            return _airconDbContext.CustomerOpportunities.Where(x => x.Id == id)
                .Include(x => x.MainAddress)
                 .Select(x => new CustomerOpportunityAdminModel
                 {
                     CustomerOpportunityId = x.Id,
                     CompanyName = x.CompanyName,
                     AdminEmail = x.AdminEmail,
                     AdminName = x.AdminName,
                     AlternateEmail = x.AlternateEmail,
                     IATANumber = x.IATANumber,
                     AdminPhoneNumber = x.AdminPhoneNumber,
                     FranchiseParent = x.FranchiseParent,
                     EinOrSsn = x.EinOrSsn,
                     SubscriptionId = x.SubscriptionId,
                     IsPaymentProcessed = x.IsPaymentProcessed,
                     IsTermsAccepted = x.IsTermsAccepted,
                     IsSetupCompleted = x.IsSetupCompleted,
                     SignedUpDateUtc = x.SignedUpDateUtc,
                     ApprovedDateUtc = x.ApprovedDateUtc,
                     AbandonedDateUtc = x.AbandonedDateUtc,
                     CallbackScheduledDateUtc = x.CallbackScheduledDateUtc,
                     InterestLevel = x.InterestLevel,
                     Status = x.Status,
                     Id = x.Id,
                     AddressId = x.AddressId,
                     NoOfBranches = x.NoOfBranches,
                     MainAddress = x.AddressId.HasValue ? new AddressModel
                     {
                         Id = x.MainAddress.Id,
                         NickName = x.MainAddress.NickName,
                         Line1 = x.MainAddress.Line1,
                         Line2 = x.MainAddress.Line2,
                         City = x.MainAddress.City,
                         State = x.MainAddress.State,
                         Zip = x.MainAddress.Zip,
                         IsActive = x.MainAddress.IsActive
                     } : new AddressModel()

                 }).SingleOrDefault();
        }


        public List<CustomerAdminModel> GetCustomers( bool isActive, bool isAll, string searchText, int recordCountCustomersProfileQueue)
        {
            var customers = _airconDbContext.Customers
                .Include(x => x.MainAddress)
                .Select(x => new CustomerAdminModel
                {
                    CustomerId = x.Id,
                    DisplayCustomerId = x.DisplayCustomerId,
                    CompanyName = x.CompanyName,
                    AdminEmail = x.AdminEmail,
                    AdminName = x.AdminName,
                    AlternateEmail = x.AlternateEmail,
                    IATANumber = x.IATANumber,
                    AdminPhoneNumber = x.AdminPhoneNumber,
                    FranchiseParent = x.FranchiseParent,
                    EinOrSsn = x.EinOrSsn,
                    SubscriptionId = x.SubscriptionId,
                    IsPaymentProcessed = x.IsPaymentProcessed,
                    IsTermsAccepted = x.IsTermsAccepted,
                    IsSetupCompleted = x.IsSetupCompleted,
                    IsActive = x.IsActive,
                    IsSubscriptionExpired = x.IsSubscriptionExpired,
                    SubscriptionExpiryDateUtc = x.SubscriptionExpiryDateUtc,
                    SignedUpDateUtc = x.SignedUpDateUtc,
                    ApprovedDateUtc = x.ApprovedDateUtc,
                    CreationDateUtc = x.CreationDateUtc,
                    ActivatedDateUtc = x.ActivatedDateUtc,
                    NoOfBranches = x.NoOfBranches,
                    CustomerOpportunityId = x.CustomerOpportunityId,
                    AddressId = x.AddressId,
                    Creditlimit = x.Creditlimit,
                    Id= x.Id,
                    MainAddress = x.AddressId.HasValue ? new AddressModel
                    {
                        Id = x.MainAddress.Id,
                        NickName = x.MainAddress.NickName,
                        Line1 = x.MainAddress.Line1,
                        Line2 = x.MainAddress.Line2,
                        City = x.MainAddress.City,
                        State = x.MainAddress.State,
                        Zip = x.MainAddress.Zip,
                        IsActive = x.MainAddress.IsActive
                    } : new AddressModel()
                }).AsQueryable();
            var customerList = new List<CustomerAdminModel>();
            if (!isAll)
            {
                customers = customers.Where(x => x.IsActive == isActive);
            }
            if (searchText != null)
            {
                customers =
                    customers.Where(x =>
                         (x.CompanyName == null ? false : x.CompanyName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.DisplayCustomerId == null ? false : x.DisplayCustomerId.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.AdminName == null ? false : x.AdminName.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.AdminEmail == null ? false : x.AdminEmail.ToUpper().Contains(searchText.ToUpper())) ||
                         (x.AdminPhoneNumber == null ? false : x.AdminPhoneNumber.Contains(searchText))
                        ).Select(y => y);
            }
            customers = customers.Take(recordCountCustomersProfileQueue);
            return customers.ToList();
        }

        public CustomerAdminModel GetCustomer(int id)
        {
            return _airconDbContext.Customers
                .Include(x => x.MainAddress)
                .Where(x => x.Id == id)
                .Select(x => new CustomerAdminModel
                {
                    CustomerId = x.Id,
                    DisplayCustomerId = x.DisplayCustomerId,
                    CompanyName = x.CompanyName,
                    AdminEmail = x.AdminEmail,
                    AdminName = x.AdminName,
                    AlternateEmail = x.AlternateEmail,
                    IATANumber = x.IATANumber,
                    AdminPhoneNumber = x.AdminPhoneNumber,
                    FranchiseParent = x.FranchiseParent,
                    EinOrSsn = x.EinOrSsn,
                    SubscriptionId = x.SubscriptionId,
                    IsPaymentProcessed = x.IsPaymentProcessed,
                    IsTermsAccepted = x.IsTermsAccepted,
                    IsSetupCompleted = x.IsSetupCompleted,
                    IsActive = x.IsActive,
                    IsSubscriptionExpired = x.IsSubscriptionExpired,
                    SubscriptionExpiryDateUtc = x.SubscriptionExpiryDateUtc,
                    SignedUpDateUtc = x.SignedUpDateUtc,
                    ApprovedDateUtc = x.ApprovedDateUtc,
                    CreationDateUtc = x.CreationDateUtc,
                    ActivatedDateUtc = x.ActivatedDateUtc,
                    NoOfBranches = x.NoOfBranches,
                    Creditlimit = x.Creditlimit,
                    Id = x.Id,
                    CustomerOpportunityId = x.CustomerOpportunityId,
                    AddressId = x.AddressId,
                    MainAddress = x.AddressId.HasValue ? new AddressModel
                    {
                        Id = x.MainAddress.Id,
                        NickName = x.MainAddress.NickName,
                        Line1 = x.MainAddress.Line1,
                        Line2 = x.MainAddress.Line2,
                        City = x.MainAddress.City,
                        State = x.MainAddress.State,
                        Zip = x.MainAddress.Zip,
                        IsActive = x.MainAddress.IsActive
                    } : new AddressModel()
                }).SingleOrDefault();

        }

        //public CustomerAdminModel GetCustomerById(int id)
        //{
        //    CustomerAdminModel viewcustomerModel = new CustomerAdminModel();
        //    viewcustomerModel = _airconDbContext.Customers
        //        .Where(x => x.Id == id)
        //        .Select(x => new CustomerAdminModel
        //        {
        //            Id = x.Id,
        //            CustomerOpportunityId = x.CustomerOpportunityId,
        //            DisplayCustomerId = x.DisplayCustomerId,
        //            CompanyName = x.CompanyName,
        //            AdminEmail = x.AdminEmail,
        //            AdminName = x.AdminName,
        //            AlternateEmail = x.AlternateEmail,
        //            IATANumber = x.IATANumber,
        //            AdminPhoneNumber = x.AdminPhoneNumber,
        //            FranchiseParent = x.FranchiseParent,
        //            EinOrSsn = x.EinOrSsn,
        //            SubscriptionId = x.SubscriptionId,
        //            IsPaymentProcessed = x.IsPaymentProcessed,
        //            IsTermsAccepted = x.IsTermsAccepted,
        //            IsSetupCompleted = x.IsSetupCompleted,
        //            IsActive = x.IsActive,
        //            IsSubscriptionExpired = x.IsSubscriptionExpired,
        //            SubscriptionExpiryDateUtc = x.SubscriptionExpiryDateUtc,
        //            CreationDateUtc = x.CreationDateUtc,
        //            ActivatedDateUtc = x.ActivatedDateUtc,
        //            SignedUpDateUtc = x.SignedUpDateUtc,
        //            ApprovedDateUtc = x.ApprovedDateUtc,                    
        //            AddressId = x.AddressId,
        //            NoOfBranches = x.NoOfBranches,               
        //        }).SingleOrDefault();
        //    return viewcustomerModel;
        //}

        public CustomerAdminModel UpdateCustomer(CustomerAdminModel updateCustomerAdminModel)
        {
            Aircon.Data.Entities.Customer customer = _airconDbContext.Customers.Find(updateCustomerAdminModel.CustomerId);
            customer.CompanyName = updateCustomerAdminModel.CompanyName;
            customer.CustomerOpportunityId = updateCustomerAdminModel.CustomerOpportunityId;
            customer.DisplayCustomerId = updateCustomerAdminModel.DisplayCustomerId;
            customer.FranchiseParent = updateCustomerAdminModel.FranchiseParent;
            customer.AdminEmail = updateCustomerAdminModel.AdminEmail;
            customer.AdminName = updateCustomerAdminModel.AdminName;
            customer.AlternateEmail = updateCustomerAdminModel.AlternateEmail;
            customer.IATANumber = updateCustomerAdminModel.IATANumber;
            customer.EinOrSsn = updateCustomerAdminModel.EinOrSsn;
            customer.SubscriptionId = updateCustomerAdminModel.SubscriptionId;
            customer.IsTermsAccepted = updateCustomerAdminModel.IsTermsAccepted;
            customer.IsPaymentProcessed = updateCustomerAdminModel.IsPaymentProcessed;
            customer.IsSetupCompleted = updateCustomerAdminModel.IsSetupCompleted;
            customer.SignedUpDateUtc = updateCustomerAdminModel.SignedUpDateUtc;
            customer.ApprovedDateUtc = updateCustomerAdminModel.ApprovedDateUtc;
            customer.IsActive = updateCustomerAdminModel.IsActive;
            customer.IsSubscriptionExpired = updateCustomerAdminModel.IsSubscriptionExpired;
            customer.SubscriptionExpiryDateUtc = updateCustomerAdminModel.SubscriptionExpiryDateUtc;
            customer.AddressId = updateCustomerAdminModel.AddressId;
            customer.AdminPhoneNumber = updateCustomerAdminModel.AdminPhoneNumber;
            customer.Creditlimit = updateCustomerAdminModel.Creditlimit;
            _airconDbContext.Customers.Update(customer);
            _airconDbContext.SaveChanges();
            return updateCustomerAdminModel;
        }

        public CustomerOpportunityAdminModel UpdateCustomerOpportunity(CustomerOpportunityAdminModel updateCustomerOppAdminModel)
        {
            CustomerOpportunity customerOpportunity = _airconDbContext.CustomerOpportunities.Find(updateCustomerOppAdminModel.CustomerOpportunityId);
            customerOpportunity.CompanyName = updateCustomerOppAdminModel.CompanyName;      
            customerOpportunity.FranchiseParent = updateCustomerOppAdminModel.FranchiseParent;
            customerOpportunity.AdminEmail = updateCustomerOppAdminModel.AdminEmail;
            customerOpportunity.AdminName = updateCustomerOppAdminModel.AdminName;
            customerOpportunity.AlternateEmail = updateCustomerOppAdminModel.AlternateEmail;
            customerOpportunity.IATANumber = updateCustomerOppAdminModel.IATANumber;
            customerOpportunity.EinOrSsn = updateCustomerOppAdminModel.EinOrSsn;
            customerOpportunity.SubscriptionId = updateCustomerOppAdminModel.SubscriptionId;
            customerOpportunity.IsTermsAccepted = updateCustomerOppAdminModel.IsTermsAccepted;
            customerOpportunity.IsPaymentProcessed = updateCustomerOppAdminModel.IsPaymentProcessed;
            customerOpportunity.IsSetupCompleted = updateCustomerOppAdminModel.IsSetupCompleted;
            customerOpportunity.SignedUpDateUtc = updateCustomerOppAdminModel.SignedUpDateUtc;
            customerOpportunity.ApprovedDateUtc = updateCustomerOppAdminModel.ApprovedDateUtc;
            customerOpportunity.AbandonedDateUtc = updateCustomerOppAdminModel.AbandonedDateUtc;
            customerOpportunity.CallbackScheduledDateUtc = updateCustomerOppAdminModel.CallbackScheduledDateUtc;
            customerOpportunity.InterestLevel = updateCustomerOppAdminModel.InterestLevel;
            customerOpportunity.Status = updateCustomerOppAdminModel.Status;
            customerOpportunity.AddressId = updateCustomerOppAdminModel.AddressId;
            customerOpportunity.AdminPhoneNumber = updateCustomerOppAdminModel.AdminPhoneNumber;           
            _airconDbContext.CustomerOpportunities.Update(customerOpportunity);
            _airconDbContext.SaveChanges();
            return updateCustomerOppAdminModel;
        }
        public void DeleteCustomerOpportunity(int id)
        {
            var users = _airconDbContext.Users
                .Where(x => x.CustomerOpportunityId == id).ToList();
            foreach (var i in users)
            {
                var user = _airconDbContext.Users.Find(i.Id);
                user.CustomerOpportunityId = null;
                _airconDbContext.Users.Update(user);
                _airconDbContext.SaveChanges();
            }
            var customerOpportunity = _airconDbContext.CustomerOpportunities.Find(id);
            _airconDbContext.CustomerOpportunities.Remove(customerOpportunity);
            _airconDbContext.SaveChanges();
        }
       public void DeleteCustomerProfile(int id)
        {
            var users = _airconDbContext.Users
                .Where(x => x.CustomerId == id).ToList();
            foreach (var i in users)
            {
                var user = _airconDbContext.Users.Find(i.Id);
                user.CustomerId = null;
                _airconDbContext.Users.Update(user);
                _airconDbContext.SaveChanges();
            }
            Aircon.Data.Entities.Customer customer = _airconDbContext.Customers.Find(id);
            _airconDbContext.Customers.Remove(customer);
            _airconDbContext.SaveChanges();
            
        }
        public void DeactiveCustomer(int id)
        {
            Aircon.Data.Entities.Customer customer = _airconDbContext.Customers.Find(id);
            customer.IsActive = false;
            _airconDbContext.Customers.Update(customer);
            _airconDbContext.SaveChanges();
        }
        public void ActivateCustomer(int id)
        {
            Aircon.Data.Entities.Customer customer = _airconDbContext.Customers.Find(id);
            customer.IsActive = true;
            customer.ActivatedDateUtc = DateTime.UtcNow;
            _airconDbContext.SaveChanges();
        }
        public void DenyCustomerOpportunity(int id)
        {
            CustomerOpportunity customerOpportunity = _airconDbContext.CustomerOpportunities.Find(id);
             customerOpportunity.Status = CustomerOpportunityStatus.Abandoned;
            _airconDbContext.SaveChanges();
        }
        public void ApproveCustomer(int id)
        {
            CustomerOpportunity customerOpportunity = _airconDbContext.CustomerOpportunities.Where(x=> x.Id == id)
                .Include(x=> x.CustomerOpportunityAddresses)
                .Include(x=> x.PaymentMethods)
                .Include(x=> x.Users).SingleOrDefault();
            Aircon.Data.Entities.Customer customer = new Aircon.Data.Entities.Customer();
            customer.CompanyName = customerOpportunity.CompanyName;
            customer.FranchiseParent = customerOpportunity.FranchiseParent;
            customer.AdminEmail = customerOpportunity.AdminEmail;
            customer.AdminName = customerOpportunity.AdminName;
            customer.AdminPhoneNumber = customerOpportunity.AdminPhoneNumber;
            customer.AlternateEmail = customerOpportunity.AlternateEmail;
            customer.IATANumber = customerOpportunity.IATANumber;
            customer.EinOrSsn = customerOpportunity.EinOrSsn;
            customer.SubscriptionId = customerOpportunity.SubscriptionId.Value;
            customer.IsTermsAccepted = customerOpportunity.IsTermsAccepted;
            customer.IsPaymentProcessed = customerOpportunity.IsPaymentProcessed;
            customer.IsSetupCompleted = customerOpportunity.IsSetupCompleted;
            customer.SignedUpDateUtc = customerOpportunity.SignedUpDateUtc;
            customer.ApprovedDateUtc = customerOpportunity.ApprovedDateUtc;
            customer.AddressId = customerOpportunity.AddressId;
            customer.NoOfBranches = customerOpportunity.NoOfBranches;
            customer.LogoId = customerOpportunity.LogoId;
            customer.Logo = customerOpportunity.Logo;
            customer.Subscription = customerOpportunity.Subscription;
            customer.CreationDateUtc = DateTime.UtcNow;
            customer.CustomerOpportunityId = customerOpportunity.Id;
            customer.IsActive = false;

            foreach(var opportutinitaddress in customerOpportunity.CustomerOpportunityAddresses)
            {
                var customerAddress = new CustomerAddress { AddressId = opportutinitaddress.AddressId, AddressTypeId = opportutinitaddress.AddressTypeId };
                customer.CustomerAddresses.Add(customerAddress);
            }


            foreach (var customerOpportunityUser in customerOpportunity.Users)
            {
                customer.Users.Add(customerOpportunityUser);
                if (!string.IsNullOrEmpty(GetDomainName(customerOpportunityUser.UserName)))
                {
                    CustomerDomain domain = new CustomerDomain { DomainName = GetDomainName(customerOpportunityUser.UserName) };
                    customer.CustomerDomains.Add(domain);
                }
            }
            _airconDbContext.Customers.Add(customer);
            customerOpportunity.Status = CustomerOpportunityStatus.Approved;
            _airconDbContext.SaveChanges();

        }

        private string GetDomainName(string email)
        {
            string host = string.Empty;
            try
            {
                MailAddress address = new MailAddress(email);
                host = address.Host;
            }
            catch 
            {
            }
            return host.ToLower();
        }
    }
}
