using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Core.Data
{
    public static class TemplateDefinitionNames
    {
        public static class QuotesExpiring
        {
            public const string Dashboard = "QuotesExpiring.Dashboard";
            public const string Email = "QuotesExpiring.Email";
            public const string Text = "QuotesExpiring.Text";
        }
        public static class BookingNeedsAttention
        {
            public const string Dashboard = "BookingNeedsAttention.Dashboard";
            public const string Email = "BookingNeedsAttention.Email";
            public const string Text = "BookingNeedsAttention.Text";
        }
        public static class ShipmentNeedsAttention
        {
            public const string Dashboard = "ShipmentNeedsAttention.Dashboard";
            public const string Email = "ShipmentNeedsAttention.Email";
            public const string Text = "ShipmentNeedsAttention.Text";
        }
        public static class ShipmentDelivered
        {
            public const string Dashboard = "ShipmentDelivered.Dashboard";
            public const string Email = "ShipmentDelivered.Email";
            public const string Text = "ShipmentDelivered.Text";
        }
        public static class ShipmentDashboardNotification
        {
            public const string Booked = "ShipmentDashboardNotification.Booked";
            public const string PickupRequested = "ShipmentDashboardNotification.PickupRequested";
            public const string TenderedToAirline = "ShipmentDashboardNotification.TenderedToAirline";
            public const string ShipmentDeparted = "ShipmentDashboardNotification.ShipmentDeparted";
            public const string ShipmentArrived = "ShipmentDashboardNotification.ShipmentArrived";

        }
        public static class General
        {
            public const string PasswordReset = "General.PasswordReset";
            public const string SignUpWelcomeEmail = "General.SignUpWelcomeEmail";
            public const string ApprovingUserEmail = "General.ApprovingUserEmail";
            public const string ActivatingCustomerEmail = "General.ActivatingCustomerEmail";
            public const string ActivatingUserEmail = "General.ActivatingUserEmail";
            public const string ApprovingCustomerEmail = "General.ApprovingCustomerEmail";

        }
        public static class Quotes
        {
            public const string AtaQuote = "Quotes.AtaQuote";
            public const string DtaQuote = "Quotes.DtaQuote";
        }
        public static class Layout
        {
            public const string EmailLayout = "Layout.EmailLayout";
        }
    }

    public static class NotificationSettingName
    {
        public static class QuotesExpiring
        {
            public const string Dashboard = "QuotesExpiringDashboard";
            public const string Email = "QuotesExpiringEmail";
            public const string Text = "QuotesExpiringText";
        }
        public static class BookingNeedsAttention
        {
            public const string Dashboard = "BookingNeedsAttentionDashboard";
            public const string Email = "BookingNeedsAttentionEmail";
            public const string Text = "BookingNeedsAttentionText";
        }
        public static class ShipmentNeedsAttention
        {
            public const string Dashboard = "ShipmentNeedsAttentionDashboard";
            public const string Email = "ShipmentNeedsAttentionEmail";
            public const string Text = "ShipmentNeedsAttentionText";
        }
        public static class ShipmentDelivered
        {
            public const string Dashboard = "ShipmentDeliveredDashboard";
            public const string Email = "ShipmentDeliveredEmail";
            public const string Text = "ShipmentDeliveredText";
        }
        public static class ShipmentDashboardNotification
        {
            public const string Booked = "ShipmentDashboardNotificationBooked";
            public const string PickupRequested = "ShipmentDashboardNotificationPickupRequested";
            public const string TenderedToAirline = "ShipmentDashboardNotificationTenderedToAirline";
            public const string ShipmentDeparted = "ShipmentDashboardNotificationShipmentDeparted";
            public const string ShipmentArrived = "ShipmentDashboardNotificationShipmentArrived";
        }
    }


}
