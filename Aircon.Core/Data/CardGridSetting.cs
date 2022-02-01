using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon
{
    public static class CardGridSetting
    {
        public static class CustomerSettings
        {
            public const string UserQueue = "CustomerSettingsUserQueue";
            public const string HideUserQueue = "CustomerSettingsHideUserQueue";
            public const string Customers = "CustomerSettingsCustomers";
            public const string HideCustomersQueue = "CustomerSettingsHideCustomerQueue";

        }

        public static class AdminUser
        {
            public const string UserQueue = "AdminUserQueue";
            public const string HideUserQueue = "AdminUserHideQueue";
            public const string Customers = "AdminUserCustomers";
            public const string HideCustomersQueue = "AdminUserHideCustomers";

        }

        public static class AdminCustomer
        {
            public const string CustomerOpportunityQueue = "AdminCustomerCustomerOpportunityQueue";
            public const string HideCustomerOpportunityQueue = "AdminCustomerHideUserQueue";
            public const string Customers = "AdminCustomers";
            public const string HideCustomersQueue = "AdminCustomersHide";

        }

        public static class Employee
        {
            public const string SystemSettingEmployee = "SystemSettingEmployee";
            public const string HideSystemSettingEmployee = "HideSystemSettingEmployee";
        }
        public static class QuotesandBookings
        {
            public const string Quotes = "QuotesQueue";
            public const string HideQuotes = "HideQuotesQueue";
            public const string Bookings = "BookingsQueue";
            public const string HideBookings = "HideBookingsQueue";

        }
    }
}
