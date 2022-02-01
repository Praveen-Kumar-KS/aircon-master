using Aircon.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.SampleData.Entity
{
    public static class CustomerDemoData
    {
        public static Customer GetDemoCustomer()
        {
            return new Customer
            {
                CompanyName = "Acme Ltd",
                CustomerDomains = new List<CustomerDomain> {
                new CustomerDomain { DomainName = "example.com" }}
            };
        }

        public static SubscriptionType GetSubscription()
        {
            return new SubscriptionType
            {
                 IsPopular = true,
                 Name = "MONTH TO MONTH",
                 MonthlyAmount = 1500,
                 AnnualAmount = 18000,
            };
        }
    }
}
