using Aircon.Data.Enums;

namespace Aircon.Business.Models.Customer.Preference
{
    public class UserPreferenceModel
    {
        public int UserId { get; set; }
        public int? PreferenceId { get; set; }
        public LandingPage LandingPage { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int WindowsTimeZoneId { get; set; }
        public string WindowsTimeZoneName { get; set; }
    }

    public class UserNotificationSettingModel
    {
        public int UserId { get; set; }
        public int UserNotificationSettingId { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public NotificationGroup NotificationGroup { get; set; }
    }

    public class UserUpdateNotificationSettingModel
    {
        public int UserId { get; set; }
        public bool ShipmentDeliveredDashboard { get; set; }
        public bool QuotesExpiringText { get; set; }
        public bool BookingNeedsAttentionDashboard { get; set; }
        public bool BookingNeedsAttentionEmail { get; set; }
        public bool BookingNeedsAttentionText { get; set; }
        public bool ShipmentNeedsAttentionDashboard { get; set; }
        public bool ShipmentNeedsAttentionEmail { get; set; }
        public bool ShipmentNeedsAttentionText { get; set; }
        public bool QuotesExpiringDashboard { get; set; }
        public bool ShipmentDeliveredEmail { get; set; }
        public bool ShipmentDeliveredText { get; set; }
        public bool ShipmentDashboardNotificationBooked { get; set; }
        public bool ShipmentDashboardNotificationPickupRequested { get; set; }
        public bool ShipmentDashboardNotificationTenderedToAirline { get; set; }
        public bool ShipmentDashboardNotificationShipmentDeparted { get; set; }
        public bool ShipmentDashboardNotificationShipmentArrived { get; set; }
        public bool QuotesExpiringEmail { get; set; }
    }

}
