using Aircon.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Core.Data
{
    public static class TemplateDefinationProvider
    {
        public static readonly TemplateDefinition QuotesExpiringDashboard = new TemplateDefinition { Name = TemplateDefinitionNames.QuotesExpiring.Dashboard };
        public static readonly TemplateDefinition QuotesExpiringEmail = new TemplateDefinition { Name = TemplateDefinitionNames.QuotesExpiring.Email };
        public static readonly TemplateDefinition QuotesExpiringText = new TemplateDefinition { Name = TemplateDefinitionNames.QuotesExpiring.Text };
        public static readonly TemplateDefinition BookingNeedsAttentionDashboard = new TemplateDefinition { Name = TemplateDefinitionNames.BookingNeedsAttention.Dashboard };
        public static readonly TemplateDefinition BookingNeedsAttentionEmail = new TemplateDefinition { Name = TemplateDefinitionNames.BookingNeedsAttention.Email };
        public static readonly TemplateDefinition BookingNeedsAttentionText = new TemplateDefinition { Name = TemplateDefinitionNames.BookingNeedsAttention.Text };
        public static readonly TemplateDefinition ShipmentNeedsAttentionDashboard = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentNeedsAttention.Dashboard };
        public static readonly TemplateDefinition ShipmentNeedsAttentionEmail = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentNeedsAttention.Email };
        public static readonly TemplateDefinition ShipmentNeedsAttentionText = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentNeedsAttention.Text };
        public static readonly TemplateDefinition ShipmentDelayedDashboard = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentDelivered.Dashboard };
        public static readonly TemplateDefinition ShipmentDelayedEmail = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentDelivered.Email };
        public static readonly TemplateDefinition ShipmentDelayedText = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentDelivered.Text };
        public static readonly TemplateDefinition ShipmentDashboardNotificationBooked = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentDashboardNotification.Booked };
        public static readonly TemplateDefinition ShipmentDashboardNotificationPickupRequested = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentDashboardNotification.PickupRequested };
        public static readonly TemplateDefinition ShipmentDashboardNotificationTenderedToAirline = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentDashboardNotification.TenderedToAirline };
        public static readonly TemplateDefinition ShipmentDashboardNotificationShipmentDeparted = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentDashboardNotification.ShipmentDeparted };
        public static readonly TemplateDefinition ShipmentDashboardNotificationShipmentArrived = new TemplateDefinition { Name = TemplateDefinitionNames.ShipmentDashboardNotification.ShipmentArrived };
        public static readonly TemplateDefinition EmailLayout = new TemplateDefinition{
            Name = TemplateDefinitionNames.Layout.EmailLayout,
            DisplayName = "Email Layout",
            TemplateText = @"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta charset=""utf-8"" />
</head>
<body>
    {{content}}
</body>
</html>",
            IsLayout = true,
            Layout = string.Empty,
            SampleTemplateText = @"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta charset=""utf-8"" />
</head>
<body>
    {{content}}
</body>
</html>",
            Instructions = string.Empty
        };
        public static readonly TemplateDefinition GeneralPasswordReset = new TemplateDefinition
        {
            Name = TemplateDefinitionNames.General.PasswordReset,
            DisplayName = "Reset Password",
            TemplateText = @"<p>Dear {{model.displayname}},<br>
<p>We have recieved your request to reset the password.
<a href='{{model.link}}' class='c-pointer h-link' >Click here</a> to set a new password.<br>
<p>Thanks,
<p>Aircon Team.</p>",
            IsLayout = false,
            Layout = TemplateDefinitionNames.Layout.EmailLayout,
            SampleTemplateText = @"<p>Dear {{model.displayname}},<br>
<p>We have recieved your request to reset the password.
<a href='{{model.link}}' class='c-pointer h-link' >Click here</a> to set a new password.<br>
<p>Thanks,
<p>Aircon Team.</p>",
            Instructions = string.Empty
        };
        public static readonly TemplateDefinition GeneralSignUpWelcomeEmail = new TemplateDefinition
        {
            Name = TemplateDefinitionNames.General.SignUpWelcomeEmail,
            DisplayName = "Confirm Email",
            TemplateText = @"<p>Dear {{model.displayname}},<br>
<p>We have recieved your request for you user account.
<a href='{{model.link}}' class='c-pointer h-link' >Click here</a> to confirm your email.<br>
<p>Thanks,
<p>Aircon Team.</p>",
            IsLayout = false,
            Layout = TemplateDefinitionNames.Layout.EmailLayout,
            SampleTemplateText = @"<p>Dear {{model.displayname}},<br>
<p>We have recieved your request for you user account.
<a href='{{model.link}}' class='c-pointer h-link' >Click here</a> to confirm your email.<br>
<p>Thanks,
<p>Aircon Team.</p>",
            Instructions = string.Empty
        };

        public static readonly TemplateDefinition ApproveUserEmail = new TemplateDefinition
        {
            Name = TemplateDefinitionNames.General.ApprovingUserEmail,
            DisplayName = "User Approval",
            TemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Your account is approved successfully.
<p>Thanks,
<p>Aircon Team.</p>",
            IsLayout = false,
            Layout = TemplateDefinitionNames.Layout.EmailLayout,
            SampleTemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Your account is approved successfully.
<p>Thanks,
<p>Aircon Team.</p>",
            Instructions = string.Empty
        };
        public static readonly TemplateDefinition ApproveCustomerEmail = new TemplateDefinition
        {
            Name = TemplateDefinitionNames.General.ApprovingCustomerEmail,
            DisplayName = "Customer Approval",
            TemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Your account is approved successfully.
<p>Thanks,
<p>Aircon Team.</p>",
            IsLayout = false,
            Layout = TemplateDefinitionNames.Layout.EmailLayout,
            SampleTemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Your account is approved successfully.
<p>Thanks,
<p>Aircon Team.</p>",
            Instructions = string.Empty
        };
        public static readonly TemplateDefinition ActivatingUserEmail = new TemplateDefinition
        {
            Name = TemplateDefinitionNames.General.ActivatingUserEmail,
            DisplayName = "User Activation",
            TemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Your account is activated successfully.
<p>Thanks,
<p>Aircon Team.</p>",
            IsLayout = false,
            Layout = TemplateDefinitionNames.Layout.EmailLayout,
            SampleTemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Your account is approved successfully.
<p>Thanks,
<p>Aircon Team.</p>",
            Instructions = string.Empty
        };

        public static readonly TemplateDefinition ActivatingCustomerEmail = new TemplateDefinition
        {
            Name = TemplateDefinitionNames.General.ActivatingCustomerEmail,
            DisplayName = "Customer Activation",
            TemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Your account is activated successfully.
<p>Thanks,
<p>Aircon Team.</p>",
            IsLayout = false,
            Layout = TemplateDefinitionNames.Layout.EmailLayout,
            SampleTemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Your account is approved successfully.
<p>Thanks,
<p>Aircon Team.</p>",
            Instructions = string.Empty
        };

        public static readonly TemplateDefinition EmailAtaQuote = new TemplateDefinition
        {
            Name = TemplateDefinitionNames.Quotes.AtaQuote,
            DisplayName = "Email Quote",
            TemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Kindly check the attachments.
<p>Thanks,
<p>Aircon Team.</p>",
            IsLayout = false,
            Layout = TemplateDefinitionNames.Layout.EmailLayout,
            SampleTemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Kindly check the attachments.
<p>Thanks,
<p>Aircon Team.</p>",
            Instructions = string.Empty
        };

        public static readonly TemplateDefinition EmailDtaQuote = new TemplateDefinition
        {
            Name = TemplateDefinitionNames.Quotes.DtaQuote,
            DisplayName = "Email Quote",
            TemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Kindly check the attachments.
<p>Thanks,
<p>Aircon Team.</p>",
            IsLayout = false,
            Layout = TemplateDefinitionNames.Layout.EmailLayout,
            SampleTemplateText = @"<p>Dear {{model.displayname}},<br>
<p>Kindly check the attachments.
<p>Thanks,
<p>Aircon Team.</p>",
            Instructions = string.Empty
        };

    }
}

