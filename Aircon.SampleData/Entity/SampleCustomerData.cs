using Aircon.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.SampleData.Entity
{
    public static class SampleCustomerData
    {
        public static Customer GetCustomer()
        {
            return new Customer
            {
                CompanyName ="Test" ,
                FranchiseParent ="Check" ,
                AdminEmail = "john.doe1@example.com",
                AdminName = "System",
                AdminPhoneNumber = "5985599",
                AlternateEmail = "john.doe1@example.com",
                IATANumber = "",
                EinOrSsn = "",
                IsTermsAccepted = true,
                IsPaymentProcessed = false,
                IsSetupCompleted = true,
                IsActive = true,
                IsSubscriptionExpired= false,
                //Creditlimit = ,
                //NoOfBranches = ,
                //Subscription = ""
            };
        }
        public static Customer GetCustomerWithCompanyName()
        {
            return new Customer
            {
                //CompanyName = "",
                FranchiseParent = "Check",
                AdminEmail = "john.doe1@example.com",
                AdminName = "System",
                AdminPhoneNumber = "5985599",
                AlternateEmail = "john.doe1@example.com",
                IATANumber = "",
                EinOrSsn = "Supervisor",
                IsTermsAccepted = true,
                IsPaymentProcessed = false,
                IsSetupCompleted = true,
                IsActive = true,
                IsSubscriptionExpired = false,
                //Creditlimit = ,
                //NoOfBranches = ,
                //Subscription = ""
            };
        }
        public static Customer GetCustomerWithFranchiseParent()
        {
            return new Customer
            {
                CompanyName = "",
                //FranchiseParent = "",
                AdminEmail = "john.doe1@example.com",
                AdminName = "System",
                AdminPhoneNumber = "Admin",
                AlternateEmail = "john.doe1@example.com",
                IATANumber = "",
                EinOrSsn = "Supervisor",
                IsTermsAccepted = true,
                IsPaymentProcessed = false,
                IsSetupCompleted = true,
                IsActive = true,
                IsSubscriptionExpired = false,
                //Creditlimit = ,
                //NoOfBranches = ,
                //Subscription = ""
            };
        }
        public static Customer GetCustomerWithAdminEmail()
        {
            return new Customer
            {
                CompanyName = "",
                FranchiseParent = "",
               // AdminEmail = "john.doe1@example.com",
                AdminName = "System",
                AdminPhoneNumber = "Admin",
                AlternateEmail = "john.doe1@example.com",
                IATANumber = "",
                EinOrSsn = "Supervisor",
                IsTermsAccepted = true,
                IsPaymentProcessed = false,
                IsSetupCompleted = true,
                IsActive = true,
                IsSubscriptionExpired = false,
                //Creditlimit = ,
                //NoOfBranches = ,
                //Subscription = ""
            };
        }
        public static Customer GetCustomerWithAdminName()
        {
            return new Customer
            {
                CompanyName = "",
                FranchiseParent = "",
                AdminEmail = "john.doe1@example.com",
                //AdminName = "System",
                AdminPhoneNumber = "Admin",
                AlternateEmail = "john.doe1@example.com",
                IATANumber = "",
                EinOrSsn = "Supervisor",
                IsTermsAccepted = true,
                IsPaymentProcessed = false,
                IsSetupCompleted = true,
                IsActive = true,
                IsSubscriptionExpired = false,
                //Creditlimit = ,
                //NoOfBranches = ,
                //Subscription = ""
            };
        }
        public static Customer GetCustomerWithAdminPhoneNumber()
        {
            return new Customer
            {
                CompanyName = "",
                FranchiseParent = "",
                AdminEmail = "john.doe1@example.com",
                AdminName = "System",
                //AdminPhoneNumber = "Admin",
                AlternateEmail = "john.doe1@example.com",
                IATANumber = "",
                EinOrSsn = "Supervisor",
                IsTermsAccepted = true,
                IsPaymentProcessed = false,
                IsSetupCompleted = true,
                IsActive = true,
                IsSubscriptionExpired = false,
                //Creditlimit = ,
                //NoOfBranches = ,
                //Subscription = ""
            };
        }
        public static Customer GetCustomerWithAlternateEmail()
        {
            return new Customer
            {
                CompanyName = "",
                FranchiseParent = "",
                AdminEmail = "john.doe1@example.com",
                AdminName = "System",
                AdminPhoneNumber = "Admin",
                //AlternateEmail = "john.doe1@example.com",
                IATANumber = "",
                EinOrSsn = "Supervisor",
                IsTermsAccepted = true,
                IsPaymentProcessed = false,
                IsSetupCompleted = true,
                IsActive = true,
                IsSubscriptionExpired = false,
                //Creditlimit = ,
                //NoOfBranches = ,
                //Subscription = ""
            };
        }
        public static Customer GetCustomerWithIATANumber()
        {
            return new Customer
            {
                CompanyName = "",
                FranchiseParent = "",
                AdminEmail = "john.doe1@example.com",
                AdminName = "System",
                AdminPhoneNumber = "Admin",
                AlternateEmail = "john.doe1@example.com",
                //IATANumber = "",
                EinOrSsn = "Supervisor",
                IsTermsAccepted = true,
                IsPaymentProcessed = false,
                IsSetupCompleted = true,
                IsActive = true,
                IsSubscriptionExpired = false,
                //Creditlimit = ,
                //NoOfBranches = ,
                //Subscription = ""
            };
        }
        public static Customer GetCustomerWithEinOrSsn()
        {
            return new Customer
            {
                CompanyName = "",
                FranchiseParent = "",
                AdminEmail = "john.doe1@example.com",
                AdminName = "System",
                AdminPhoneNumber = "Admin",
                AlternateEmail = "john.doe1@example.com",
                IATANumber = "",
                //EinOrSsn = "Supervisor",
                IsTermsAccepted = true,
                IsPaymentProcessed = false,
                IsSetupCompleted = true,
                IsActive = true,
                IsSubscriptionExpired = false,
                //Creditlimit = ,
                //NoOfBranches = ,
                //Subscription = ""
            };
        }
    }
}
