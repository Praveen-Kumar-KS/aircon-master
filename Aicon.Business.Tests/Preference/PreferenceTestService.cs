using Aircon.Business.Seeder;
using Aircon.Business.Services.Customer;
using Aircon.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Aircon.Business.Models.Customer.Preference;
using Aircon.Data.Enums;
using Aircon.SampleData.Bogus;
using Aircon.Extensions;

namespace Aicon.Business.UnitTests.Preference
{
    [Collection("AirconWebApplicationFactory")]
    public class PreferenceTestService : IntegrationTestBase, IDisposable
    {
        private readonly IPreferenceService _preferenceService;
        private Aircon.Data.Entities.Preference Preference;
        private Aircon.Data.Entities.NotificationSetting Notification;

        public PreferenceTestService(AirconWebApplicationFactory factory) : base(factory)
        {
            _preferenceService = GetRequiredService<IPreferenceService>();
            var preferences = BogusCustomerData.GetPreference().FirstOrDefault();
            AirconDbContext.Preferences.Add(Preference);
            AirconDbContext.SaveChanges();

            _preferenceService = GetRequiredService<IPreferenceService>();
            var notifications = BogusCustomerData.GetNotificationSetting().FirstOrDefault();
            AirconDbContext.NotificationSettings.Add(Notification);
            AirconDbContext.SaveChanges();
        }

        [Fact]
        public void GetCurrentPreference_ListDetails()
        {
            var result = _preferenceService.GetCurrentPreference(Preference.Id);
            Assert.Equal(Preference.Id, result.UserId);
        }
        [Fact]
        public void GetCurrentNotificationSettings_ListOptions()
        {
            var result = _preferenceService.GetCurrentNotificationSettings(Notification.Id);
            Assert.Equal(Notification.Id, result.UserId);
        }
        [Fact]
        public void SaveCurrentNotificationSettings_All_SaveSelected()
        {
            var save = _preferenceService.GetCurrentNotificationSettings(Notification.Id).ToViewModel();
            save.UserId = Notification.Id;
            save.BookingNeedsAttentionText = true;
            save.QuotesExpiringText = true;
            save.BookingNeedsAttentionDashboard = false;
            save.BookingNeedsAttentionEmail = true;
            save.BookingNeedsAttentionText = true;
            save.ShipmentNeedsAttentionDashboard = true;
            save.ShipmentNeedsAttentionEmail = true;
            save.ShipmentNeedsAttentionText = true;
            var saveresult = _preferenceService.SaveCurrentNotificationSettings(save.ToModel());
            Assert.Equal(Notification.Id, saveresult.UserId);
            Assert.Equal(save.ShipmentDeliveredDashboard, saveresult.ShipmentDeliveredDashboard);
            Assert.True(saveresult.QuotesExpiringText);
            Assert.False(saveresult.BookingNeedsAttentionDashboard);
            Assert.True(saveresult.BookingNeedsAttentionEmail);
            Assert.True(saveresult.BookingNeedsAttentionText);
            Assert.True(saveresult.ShipmentNeedsAttentionDashboard);
            Assert.True(saveresult.ShipmentNeedsAttentionEmail);
            Assert.True(saveresult.ShipmentNeedsAttentionText);
        }
        [Fact]
        public void SaveCurrentPreference_SaveAll_Selected()
        {
            var Save = new UserPreferenceModel();
            Save.UserId = Preference.Id;
            Save.PreferenceId = Preference.Id;
            Save.LandingPage= Preference.LandingPage;
            Save.MeasurementUnit = Preference.MeasurementUnit;
            Save.CountryId = Preference.CountryId;
            Save.CountryName = Preference.Country.CountryName;
            Save.WindowsTimeZoneId = Preference.WindowsTimeZoneId;
            Save.WindowsTimeZoneName = Preference.WindowsTimeZone.Name;          
            var saveresult = _preferenceService.SaveCurrentPreference(Save);
            Assert.Equal(Save.UserId, saveresult.UserId);
            Assert.Equal(Save.PreferenceId, saveresult.PreferenceId);
            Assert.Equal(Save.LandingPage, saveresult.LandingPage);
            Assert.Equal(Save.MeasurementUnit, saveresult.MeasurementUnit);
            Assert.Equal(Save.CountryId, saveresult.CountryId);
            Assert.Equal(Save.CountryName, saveresult.CountryName);
            Assert.Equal(Save.WindowsTimeZoneId, saveresult.WindowsTimeZoneId);
            Assert.Equal(Save.WindowsTimeZoneName, saveresult.WindowsTimeZoneName);
         }
        public void Dispose()
        {
            AirconDbContext.Preferences.Remove(Preference);
            AirconDbContext.NotificationSettings.Remove(Notification);
        }

    }
}
