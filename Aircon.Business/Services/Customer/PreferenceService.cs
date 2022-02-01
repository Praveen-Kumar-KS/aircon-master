using Aircon.Business.Models.Customer.Preference;
using Aircon.Data;
using Aircon.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Aircon.Business.Services.Customer
{
    public interface IPreferenceService
    {
        UserPreferenceModel GetCurrentPreference(int userId);
        UserUpdateNotificationSettingModel GetCurrentNotificationSettings(int userId);
        UserUpdateNotificationSettingModel SaveCurrentNotificationSettings(UserUpdateNotificationSettingModel userUpdateNotificationSettingModel);
        UserPreferenceModel SaveCurrentPreference(UserPreferenceModel userPreferenceModel);
    }


    public class PreferenceService : IPreferenceService
    {

        private readonly AirconDbContext _airconDbContext;

        public PreferenceService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }
        public UserPreferenceModel GetCurrentPreference(int userId)
        {
            var result = new UserPreferenceModel();
            var userPref = _airconDbContext.Users.Include(x => x.UserPreference).Where(x => x.Id == userId).SingleOrDefault();
            if (userPref.UserPreference == null)
            {
                var defaultPreference = _airconDbContext.SystemSettings.Include(x => x.DefaultPreference).ThenInclude(x => x.Country)
                    .Include(x => x.DefaultPreference).ThenInclude(x => x.WindowsTimeZone)
                    .ToList().FirstOrDefault().DefaultPreference;
                var userPreference = new Preference
                {
                    LandingPage = defaultPreference.LandingPage,
                    MeasurementUnit = defaultPreference.MeasurementUnit,
                    CountryId = defaultPreference.CountryId,
                    WindowsTimeZoneId = defaultPreference.WindowsTimeZoneId
                };
                userPref.UserPreference = userPreference;
                _airconDbContext.Users.Update(userPref);
                _airconDbContext.SaveChanges();
            }
            var data = _airconDbContext.Users.Where(x => x.Id == userId).Include(x => x.UserPreference).ThenInclude(x => x.Country)
                .Include(x => x.UserPreference).ThenInclude(x => x.WindowsTimeZone)
                .ToList().FirstOrDefault();
            result = new UserPreferenceModel
            {
                UserId = data.Id,
                PreferenceId = data.UserPreference.Id,
                LandingPage = data.UserPreference.LandingPage,
                MeasurementUnit = data.UserPreference.MeasurementUnit,
                CountryId = data.UserPreference.CountryId,
                WindowsTimeZoneId = data.UserPreference.WindowsTimeZoneId,
                CountryName = data.UserPreference.Country.CountryName,
                WindowsTimeZoneName = data.UserPreference.WindowsTimeZone.Name
            };
            return result;
        }
        public UserUpdateNotificationSettingModel GetCurrentNotificationSettings(int userId)
        {
            var userUpdateNotificationSettingModel = new UserUpdateNotificationSettingModel();
            var userNotificationSettings = _airconDbContext.Users.Include(x => x.UserNotificationSettings).Where(x => x.Id == userId).SingleOrDefault();
            var defaultNotificationSettings = _airconDbContext.SystemSettings.Include(x => x.DefaultNotificationSettings).FirstOrDefault();
            var missingDefaultNotificationSettings = defaultNotificationSettings.DefaultNotificationSettings.Where(p => !userNotificationSettings.UserNotificationSettings.Any(p2 => p2.NotificationSettingId == p.NotificationSettingId));

            foreach(var notificationSetting in missingDefaultNotificationSettings)
            {
                _airconDbContext.UserNotificationSettings.Add(new UserNotificationSetting { Id = userId, IsActive = notificationSetting.IsActive, NotificationSettingId = notificationSetting.NotificationSettingId });
            }
            _airconDbContext.SaveChanges();

            var result = _airconDbContext.UserNotificationSettings
                .Where(x => x.Id == userId)
                .Include(x=> x.NotificationSetting)
                .Select(x=> new UserNotificationSettingModel
                {
                    UserId = x.Id,
                    UserNotificationSettingId = x.NotificationSetting.Id,
                    IsActive = x.IsActive,
                    Name = x.NotificationSetting.Name,
                    SystemName = x.NotificationSetting.SystemName,
                    NotificationGroup = x.NotificationSetting.NotificationGroup
                }).ToList();
            userUpdateNotificationSettingModel = LoadSetting(result, userId);
            return userUpdateNotificationSettingModel;
        }

        public UserPreferenceModel SaveCurrentPreference(UserPreferenceModel userPreferenceModel)
        {
            var preference = _airconDbContext.Users.Where(x => x.Id == userPreferenceModel.UserId).Include(x=> x.UserPreference).SingleOrDefault().UserPreference;
            preference.LandingPage = userPreferenceModel.LandingPage;
            preference.MeasurementUnit = userPreferenceModel.MeasurementUnit;
            preference.CountryId = userPreferenceModel.CountryId;
            preference.WindowsTimeZoneId = userPreferenceModel.WindowsTimeZoneId;

            _airconDbContext.SaveChanges();
            return userPreferenceModel;
        }

        public UserUpdateNotificationSettingModel SaveCurrentNotificationSettings(UserUpdateNotificationSettingModel userUpdateNotificationSettingModel)
        {
            var userNotificationSettings = _airconDbContext.Users.Include(x => x.UserNotificationSettings).ThenInclude(x=> x.NotificationSetting).Where(x => x.Id == userUpdateNotificationSettingModel.UserId).SingleOrDefault();
            var result = SaveSetting(userUpdateNotificationSettingModel, userNotificationSettings.UserNotificationSettings.ToList(), userUpdateNotificationSettingModel.UserId);
            foreach(var userNotificationSetting in result)
            {
                _airconDbContext.UserNotificationSettings.Update(userNotificationSetting);
            }
            _airconDbContext.SaveChanges();

            return userUpdateNotificationSettingModel;
        }

        private UserUpdateNotificationSettingModel LoadSetting(List<UserNotificationSettingModel> userNotificationSettings,  int userId)
        {
            Type type = typeof(UserUpdateNotificationSettingModel);
            var _userUpdateNotificationSettingModel = new UserUpdateNotificationSettingModel();
            _userUpdateNotificationSettingModel.UserId = userId;
            foreach (var prop in type.GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = prop.Name;
                var setting = userNotificationSettings.Where(x => x.SystemName == key).SingleOrDefault()?.IsActive.ToString(); 
                if (setting == null)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
                    continue;

                var value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);
                //set property
                prop.SetValue(_userUpdateNotificationSettingModel, value, null);
            }

            return _userUpdateNotificationSettingModel;
        }

        private List<UserNotificationSetting> SaveSetting(UserUpdateNotificationSettingModel userUpdateNotificationSettingModel,List<UserNotificationSetting> userNotificationSettings, int userId)
        {
            Type type = typeof(UserUpdateNotificationSettingModel);
            foreach (var prop in type.GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = prop.Name;
                var setting = userNotificationSettings.Where(x => x.NotificationSetting.SystemName == key)?.SingleOrDefault();
                if (setting == null)
                    continue;
                setting.IsActive = (bool) userUpdateNotificationSettingModel.GetType().GetProperty(key).GetValue(userUpdateNotificationSettingModel, null);

            }

            return userNotificationSettings;
        }
    }
}
