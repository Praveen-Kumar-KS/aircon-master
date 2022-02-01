using Aircon.Business.Models.Shared;
using Aircon.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aircon.Business.Services
{
    public interface ICommonService
    {
        string EnryptString(string str);
        string DecryptString(string encrString);
        List<SelectListItem> CustomerRoles();
        List<SubscriptionTypeModel> SubscriptionTypes();
    }

    public class CommonService : ICommonService
    {
        private readonly AirconDbContext _airconDBContext;
        public CommonService(AirconDbContext airconDbContext)
        {
            _airconDBContext = airconDbContext;
        } 

        public string EnryptString(string str)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        public string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
                throw fe.InnerException;
            }
            return decrypted;
        }

        public List<SelectListItem> CustomerRoles()
        {
            var customerRoles = new List<SelectListItem>();
            customerRoles = _airconDBContext.Roles
                .Where(x => x.IsSystemRole == false)
                .Where(x => x.Active == true)
                .Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Name }).ToList();
            return customerRoles;

        }

        public List<SubscriptionTypeModel> SubscriptionTypes()
        {
            var subscriptionTypes = new List<SubscriptionTypeModel>();
            subscriptionTypes = _airconDBContext.SubscriptionTypes
               .Where(x => x.Active == true)
                .Select(x => new SubscriptionTypeModel 
                {
                    IsPopular = x.IsPopular,
                    Line1 = x.Line1,
                    Line2 = x.Line2,
                    Line3 = x.Line3,
                    Line4 = x.Line4,
                    Line5 = x.Line5,
                    Active = x.Active,
                    AnnualAmount = x.AnnualAmount,
                    MonthlyAmount = x.MonthlyAmount,
                    Id = x.Id,
                    Name = x.Name,
                    Description =x.Description,
                    DisplayOrder = x.DisplayOrder
                }).OrderBy(x=> x.DisplayOrder).ToList();
            return subscriptionTypes;

        }
    }
}
