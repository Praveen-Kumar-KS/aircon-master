using System;
using System.Linq;
using System.Net.Mail;
using Aircon.Core;
using Aircon.Data;
using Microsoft.EntityFrameworkCore;
using Aircon.Data.Entities;
using Aircon.Business.Models.Shared;
using Aircon.Business.Models.Identity.SignUp;
using Microsoft.AspNetCore.Identity;
using Aircon.Areas.Identity.Models.SignUp;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Aircon.Data.Helper;

namespace Aircon.Business.Services.Identity
{
    public interface ISignUpService
    {
        CustomerModel CheckCustomerDomain(string email);
        SignUpCompanyProfileModel AddCustomerOpportunity(SignUpCompanyProfileModel model);
        CustomerOpportunityAddressModel UpdateAddressForCustomerOpportunity(CustomerOpportunityAddressModel model);
        IdNamePair GetUserByName(string username);
        UserDetailModel UpdateSignUpUser(UserDetailModel viewUserModel);
        CustomerOpportunityAddressModel AddAddressForCustomerOpportunity(CustomerOpportunityAddressModel model);
        CustomerOpportunityAddressModel DeleteAddressForCustomerOpportunity(CustomerOpportunityAddressModel model);
        SubscriptionPageModel AddSubscriptionPlan(SubscriptionPageModel subscriptionPageModel);
        OpportunityPaymentMethodModel AddOpportunityPaymentMethod(OpportunityPaymentMethodModel model);
        OpportunityPaymentMethodModel DeleteOpportunityPaymentMethod(OpportunityPaymentMethodModel model);
        TermsAndConditionsModel UpdateTermsAndConditions(TermsAndConditionsModel model);
        List<SelectListItem> GetCustomerBranches(int custId);

    }
    
    public class SignUpService : ISignUpService
    {
        private readonly AirconDbContext _airconDbContext;
        private readonly UserManager<User> _userManager;

        public SignUpService(AirconDbContext airconDbContext, UserManager<User> userManager)
        {
            _airconDbContext = airconDbContext;
            _userManager = userManager;
        }
        
        public CustomerModel CheckCustomerDomain(string email)
        {
            string host = string.Empty;
            var model = new CustomerModel();
            try
            {
                MailAddress address = new MailAddress(email);
                host = address.Host; 
            }catch (Exception ex)
            {
                throw new AppException("Not an email address");
            }

            var result = _airconDbContext.CustomerDomains.Where(x => x.DomainName.ToLower() == host.ToLower()).Include(x=> x.Customer).SingleOrDefault();
            if ( result != null)
            {
                model.Id = result.Customer.Id;
                model.CompanyName = result.Customer.CompanyName;
            }
            return model;
        }

        public SignUpCompanyProfileModel AddCustomerOpportunity(SignUpCompanyProfileModel model)
        {
            CustomerOpportunity customerOpportunity = new CustomerOpportunity();
            customerOpportunity.CompanyName = model.CompanyName;
            customerOpportunity.FranchiseParent = model.FranchiseParent;
            customerOpportunity.AdminEmail = model.AdminEmail;
            customerOpportunity.AlternateEmail = model.AlternateEmail;
            customerOpportunity.IATANumber = model.IATANumber;
            customerOpportunity.EinOrSsn = model.EinOrSsn;
            customerOpportunity.Status = Data.Enums.CustomerOpportunityStatus.CompanyAddress;
            _airconDbContext.CustomerOpportunities.Add(customerOpportunity);
            _airconDbContext.SaveChanges();
            model.Id = customerOpportunity.Id;
            var user = _airconDbContext.Users.Where(x => x.Id == HttpContextHelper.UserId).SingleOrDefault();
            if (user!= null)
            {
                user.CustomerOpportunityId = customerOpportunity.Id;
                _airconDbContext.SaveChanges();
            }

            return model;
        }

        public CustomerOpportunityAddressModel UpdateAddressForCustomerOpportunity(CustomerOpportunityAddressModel model)
        {
            CustomerOpportunity customerOpportunity = _airconDbContext.CustomerOpportunities.Where(x => x.Id == model.CustomerOpportunityId).SingleOrDefault();
            if (customerOpportunity != null)
            {
                customerOpportunity.MainAddress = new Address { Line1 = model.Line1, Line2 = model.Line2, City = model.City, State = model.State, Zip = model.Zip };
                customerOpportunity.LogoId = model.Logo.Id.HasValue ? model.Logo.Id.Value == 0 ? null : model.Logo.Id.Value : null;
                customerOpportunity.Status = Data.Enums.CustomerOpportunityStatus.CompanyOtherAddress;

                _airconDbContext.Update(customerOpportunity);
            }
            _airconDbContext.SaveChanges();
            return model;
        }

        public CustomerOpportunityAddressModel AddAddressForCustomerOpportunity(CustomerOpportunityAddressModel model)
        {
            CustomerOpportunity customerOpportunity = _airconDbContext.CustomerOpportunities.Where(x => x.Id == model.CustomerOpportunityId).SingleOrDefault();
            if (customerOpportunity != null)
            {
                customerOpportunity.CustomerOpportunityAddresses.Add( new CustomerOpportunityAddress
                {
                    AddressTypeId = model.AddressTypeId,
                    Address = new Address {NickName = model.NickName, Line1 = model.Line1, Line2 = model.Line2, City = model.City, State = model.State, Zip = model.Zip }
                });
                customerOpportunity.Status = Data.Enums.CustomerOpportunityStatus.SubscriptionPlan;

                _airconDbContext.Update(customerOpportunity);
            }
            _airconDbContext.SaveChanges();
            return model;
        }

        public CustomerOpportunityAddressModel DeleteAddressForCustomerOpportunity(CustomerOpportunityAddressModel model)
        {
            CustomerOpportunity customerOpportunity = _airconDbContext.CustomerOpportunities.Where(x => x.Id == model.CustomerOpportunityId).SingleOrDefault();
            if (customerOpportunity != null)
            {
                var address = _airconDbContext.CustomerOpportunityAddresses
                    .Where(x => x.Id == model.CustomerOpportunityId)
                    .Where(x => x.AddressId == model.Id)
                    .Where(x => x.AddressTypeId == model.AddressTypeId).SingleOrDefault();
                if(address != null)
                {
                    _airconDbContext.CustomerOpportunityAddresses.Remove(address);
                }
            }
            customerOpportunity.Status = Data.Enums.CustomerOpportunityStatus.SubscriptionPlan;
            _airconDbContext.SaveChanges();
            return model;
        }

        public SubscriptionPageModel AddSubscriptionPlan(SubscriptionPageModel model)
        {
            CustomerOpportunity customerOpportunity = _airconDbContext.CustomerOpportunities.Where(x => x.Id == model.CustomerOpportunityId).SingleOrDefault();
            if (customerOpportunity != null)
            {
                customerOpportunity.NoOfBranches = model.NoOfBranches;
                customerOpportunity.SubscriptionId = model.SubscriptionTypeId;
                customerOpportunity.Status = Data.Enums.CustomerOpportunityStatus.SetupPaymentMethod;

                _airconDbContext.SaveChanges();
            }
            return model;
        }


        public OpportunityPaymentMethodModel AddOpportunityPaymentMethod(OpportunityPaymentMethodModel model)
        {
            CustomerOpportunity customerOpportunity = _airconDbContext.CustomerOpportunities.Where(x => x.Id == model.CustomerOpportunityId).SingleOrDefault();
            if (customerOpportunity != null)
            {
                //customerOpportunity.NoOfBranches = model.NoOfBranches;
                //customerOpportunity.SubscriptionId = model.SubscriptionTypeId;
                customerOpportunity.Status = Data.Enums.CustomerOpportunityStatus.TermsAndConditions;

                _airconDbContext.SaveChanges();
            }
            return model;
        }


        public OpportunityPaymentMethodModel DeleteOpportunityPaymentMethod(OpportunityPaymentMethodModel model)
        {
            CustomerOpportunity customerOpportunity = _airconDbContext.CustomerOpportunities.Where(x => x.Id == model.CustomerOpportunityId).SingleOrDefault();
            if (customerOpportunity != null)
            {
                //customerOpportunity.NoOfBranches = model.NoOfBranches;
                //customerOpportunity.SubscriptionId = model.SubscriptionTypeId;
                customerOpportunity.Status = Data.Enums.CustomerOpportunityStatus.TermsAndConditions;
                _airconDbContext.SaveChanges();
            }
            return model;
        }

        public TermsAndConditionsModel UpdateTermsAndConditions(TermsAndConditionsModel model)
        {
            CustomerOpportunity customerOpportunity = _airconDbContext.CustomerOpportunities.Where(x => x.Id == model.CustomerOpportunityId).SingleOrDefault();
            if (customerOpportunity != null)
            {
                customerOpportunity.IsTermsAccepted = model.IsTermsAccepted;
                customerOpportunity.Status = Data.Enums.CustomerOpportunityStatus.CallbackScheduled;

                _airconDbContext.SaveChanges();
            }
            return model;
        }
        public IdNamePair GetUserByName(string username)
        {
            var user = _airconDbContext.Users.Where(x => x.UserName == username).Select(x => new IdNamePair
            {
                Name = x.UserName,
                Id = x.Id
            }).SingleOrDefault();
            return user;
        }
        public UserDetailModel UpdateSignUpUser(UserDetailModel viewUserModel)
        {
            User user = _airconDbContext.Users.Find(viewUserModel.UserId);
            user.FirstName = viewUserModel.FirstName;
            user.LastName = viewUserModel.LastName;
            user.WorkTitle = viewUserModel.Role;
            user.PhoneNumber = viewUserModel.Phone;
            user.CustomerId = viewUserModel.CompanyId  == 0 ? null : viewUserModel.CompanyId;
            user.AvatarId = viewUserModel.Avatar.Id.HasValue ? viewUserModel.Avatar.Id.Value == 0 ? null : viewUserModel.Avatar.Id.Value : null;
            user.SignedUpDateUtc = DateTime.UtcNow;
            user.CreationDateUtc = DateTime.UtcNow;

            user.UserStatus = Data.Enums.UserStatus.AwaitingReview;
            var result = _userManager.UpdateAsync(user).Result;
            _airconDbContext.SaveChangesAsync();
            return viewUserModel;
        }
        public List<SelectListItem> GetCustomerBranches(int custId)
        {
            var branches = _airconDbContext.CustomerAddresses.Where(x => x.Id == custId).Include(x => x.Address).Select(x => new SelectListItem
            {
                Text = x.Address.NickName,
                Value = x.AddressId.ToString()
            }).ToList();
            return branches;
        }

    }
}
