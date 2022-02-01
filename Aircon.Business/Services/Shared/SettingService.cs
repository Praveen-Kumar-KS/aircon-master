using Aircon.Business.Models.Shared;
using Aircon.Core;
using Aircon.Data;
using Aircon.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Services.Shared
{
    public interface ISettingService
    {
        Task<SettingModel> GetSettingByIdAsync(int settingId);
        Task DeleteSettingAsync(SettingModel setting);
        Task DeleteSettingsAsync(IList<SettingModel> settings);
        Task<SettingModel> GetSettingAsync(string key, int userId = 0, bool loadSharedValueIfNotFound = false);
        
        Task<T> GetSettingByKeyAsync<T>(string key, T defaultValue = default,
            int userId = 0, bool loadSharedValueIfNotFound = false);
        Task SetSettingAsync<T>(string key, T value, int userId = 0, bool clearCache = true);
        Task<IList<SettingModel>> GetAllSettingsAsync();
        Task<bool> SettingExistsAsync<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector, int userId = 0)
            where T : ISettings, new();
        Task<T> LoadSettingAsync<T>(int userId = 0) where T : ISettings, new();
        Task<ISettings> LoadSettingAsync(Type type, int userId = 0);
        Task SaveSettingAsync<T>(T settings, int userId = 0) where T : ISettings, new();
        Task SaveSettingAsync<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector,
            int userId = 0, bool clearCache = true) where T : ISettings, new();
        Task SaveSettingOverridablePerStoreAsync<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector,
            bool overrideForStore, int userId = 0, bool clearCache = true) where T : ISettings, new();

        Task DeleteSettingAsync<T>() where T : ISettings, new();

        Task DeleteSettingAsync<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector, int userId = 0) where T : ISettings, new();

        //Task ClearCacheAsync();

        string GetSettingKey<TSettings, T>(TSettings settings, Expression<Func<TSettings, T>> keySelector)
            where TSettings : ISettings, new();

    }


    public class SettingService : ISettingService
    {

        private readonly AirconDbContext _airconDbContext;


        public SettingService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        protected virtual async Task<IDictionary<string, IList<SettingModel>>> GetAllSettingsDictionaryAsync()
        {

            var settings = await GetAllSettingsAsync();

            var dictionary = new Dictionary<string, IList<SettingModel>>();
            foreach (var s in settings)
            {
                var resourceName = s.Name.ToLowerInvariant();
                var settingForCaching = new SettingModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Value = s.Value,
                    UserId = s.UserId
                };
                if (!dictionary.ContainsKey(resourceName))
                    //first setting
                    dictionary.Add(resourceName, new List<SettingModel>
                        {
                            settingForCaching
                        });
                else
                    //already added
                    //most probably it's the setting with the same name but for some certain store (userId > 0)
                    dictionary[resourceName].Add(settingForCaching);
            }

            return dictionary;

            //return await _staticCacheManager.GetAsync(NopConfigurationDefaults.SettingsAllAsDictionaryCacheKey, async () =>
            //{
            //});
        }

        protected virtual async Task SetSettingAsync(Type type, string key, object value, int userId = 0, bool clearCache = true)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            key = key.Trim().ToLowerInvariant();
            var valueStr = TypeDescriptor.GetConverter(type).ConvertToInvariantString(value);

            var setting = await _airconDbContext.Settings.Where(x => x.Name == key).Where(x => x.UserId == userId).SingleOrDefaultAsync();

            if (setting != null)
            {
                //update
                setting.Value = valueStr;
                await _airconDbContext.SaveChangesAsync();
            }
            else
            {
                //insert
                var newsetting = new Setting
                {
                    Name = key,
                    Value = valueStr,
                    UserId = userId
                };
                _airconDbContext.Settings.Add(newsetting);
                await _airconDbContext.SaveChangesAsync();
            }
        }



        //public virtual async Task InsertSettingAsync(Setting setting, bool clearCache = true)
        //{
        //    await _settingRepository.InsertAsync(setting);

        //    //cache
        //    if (clearCache)
        //        await ClearCacheAsync();
        //}

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        //public virtual async Task UpdateSettingAsync(Setting setting, bool clearCache = true)
        //{
        //    if (setting == null)
        //        throw new ArgumentNullException(nameof(setting));

        //    await _settingRepository.UpdateAsync(setting);

        //    //cache
        //    if (clearCache)
        //        await ClearCacheAsync();
        //}

        public virtual async Task DeleteSettingAsync(SettingModel setting)
        {
           var result = await _airconDbContext.Settings.Where(x => x.Id == setting.Id).SingleOrDefaultAsync();
            if (result != null)
            {
                _airconDbContext.Settings.Remove(result);
            }
            await _airconDbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteSettingsAsync(IList<SettingModel> settings)
        {
            foreach(var model in settings)
            {
                await DeleteSettingAsync(model);
            }
        }

        public virtual async Task<SettingModel> GetSettingByIdAsync(int settingId)
        {
            return await _airconDbContext.Settings.Where(x=> x.Id == settingId).Select(x=> new SettingModel { Id=x.Id, Name = x.Name, UserId = x.UserId , Value = x.Value }).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Get setting by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="userId">Store identifier</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the setting
        /// </returns>
        public virtual async Task<SettingModel> GetSettingAsync(string key, int userId = 0, bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            var settings = await GetAllSettingsDictionaryAsync();
            key = key.Trim().ToLowerInvariant();
            if (!settings.ContainsKey(key))
                return null;

            var settingsByKey = settings[key];
            var setting = settingsByKey.FirstOrDefault(x => x.UserId == userId);

            //load shared value?
            if (setting == null && userId > 0 && loadSharedValueIfNotFound)
                setting = settingsByKey.FirstOrDefault(x => x.UserId == 0);

            return setting != null ? await GetSettingByIdAsync(setting.Id) : null;
        }

        /// <summary>
        /// Get setting value by key
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="userId">Store identifier</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the setting value
        /// </returns>
        public virtual async Task<T> GetSettingByKeyAsync<T>(string key, T defaultValue = default,
            int userId = 0, bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;

            var settings = await GetAllSettingsDictionaryAsync();
            key = key.Trim().ToLowerInvariant();
            if (!settings.ContainsKey(key))
                return defaultValue;

            var settingsByKey = settings[key];
            var setting = settingsByKey.FirstOrDefault(x => x.UserId == userId);

            //load shared value?
            if (setting == null && userId > 0 && loadSharedValueIfNotFound)
                setting = settingsByKey.FirstOrDefault(x => x.UserId == 0);

            return setting != null ? CommonHelper.To<T>(setting.Value) : defaultValue;
        }

        /// <summary>
        /// Set setting value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="userId">Store identifier</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task SetSettingAsync<T>(string key, T value, int userId = 0, bool clearCache = true)
        {
            await SetSettingAsync(typeof(T), key, value, userId, clearCache);
        }

        public virtual async Task<IList<SettingModel>> GetAllSettingsAsync()
        {
            var settings = await _airconDbContext.Settings.Select(x => new SettingModel { Id = x.Id, Name = x.Name, UserId = x.UserId, Value = x.Value }).OrderBy(x=> x.Name).ToListAsync();
            return settings;
        }

        public virtual async Task<bool> SettingExistsAsync<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector, int userId = 0)
            where T : ISettings, new()
        {
            var key = GetSettingKey(settings, keySelector);

            var setting = await GetSettingByKeyAsync<string>(key, userId: userId);
            return setting != null;
        }

        /// <summary>
        /// Load settings
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="userId">Store identifier for which settings should be loaded</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<T> LoadSettingAsync<T>(int userId = 0) where T : ISettings, new()
        {
            return (T)await LoadSettingAsync(typeof(T), userId);
        }

        /// <summary>
        /// Load settings
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="userId">Store identifier for which settings should be loaded</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<ISettings> LoadSettingAsync(Type type, int userId = 0)
        {
            var settings = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = type.Name + "." + prop.Name;
                //load by store
                var setting = await GetSettingByKeyAsync<string>(key, userId: userId, loadSharedValueIfNotFound: true);
                if (setting == null)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
                    continue;

                var value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                //set property
                prop.SetValue(settings, value, null);
            }

            return settings as ISettings;
        }

        /// <summary>
        /// Save settings object
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="userId">Store identifier</param>
        /// <param name="settings">Setting instance</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task SaveSettingAsync<T>(T settings, int userId = 0) where T : ISettings, new()
        {
            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                var value = prop.GetValue(settings, null);
                if (value != null)
                    await SetSettingAsync(prop.PropertyType, key, value, userId, false);
                else
                    await SetSettingAsync(key, string.Empty, userId, false);
            }

            //and now clear cache
            //await ClearCacheAsync();
        }

        /// <summary>
        /// Save settings object
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="userId">Store ID</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task SaveSettingAsync<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector,
            int userId = 0, bool clearCache = true) where T : ISettings, new()
        {
            if (keySelector.Body is not MemberExpression member)
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");

            var key = GetSettingKey(settings, keySelector);
            var value = (TPropType)propInfo.GetValue(settings, null);
            if (value != null)
                await SetSettingAsync(key, value, userId, clearCache);
            else
                await SetSettingAsync(key, string.Empty, userId, clearCache);
        }

        /// <summary>
        /// Save settings object (per store). If the setting is not overridden per store then it'll be delete
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="overrideForStore">A value indicating whether to setting is overridden in some store</param>
        /// <param name="userId">Store ID</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task SaveSettingOverridablePerStoreAsync<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector,
            bool overrideForStore, int userId = 0, bool clearCache = true) where T : ISettings, new()
        {
            if (overrideForStore || userId == 0)
                await SaveSettingAsync(settings, keySelector, userId, clearCache);
            else if (userId > 0)
                await DeleteSettingAsync(settings, keySelector, userId);
        }

        /// <summary>
        /// Delete all settings
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteSettingAsync<T>() where T : ISettings, new()
        {
            var settingsToDelete = new List<SettingModel>();
            var allSettings = await GetAllSettingsAsync();
            foreach (var prop in typeof(T).GetProperties())
            {
                var key = typeof(T).Name + "." + prop.Name;
                settingsToDelete.AddRange(allSettings.Where(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
            }

            await DeleteSettingsAsync(settingsToDelete);
        }

        /// <summary>
        /// Delete settings object
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="userId">Store ID</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteSettingAsync<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector, int userId = 0) where T : ISettings, new()
        {
            var key = GetSettingKey(settings, keySelector);
            key = key.Trim().ToLowerInvariant();

            var allSettings = await GetAllSettingsDictionaryAsync();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault(x => x.UserId == userId) : null;
            if (settingForCaching == null)
                return;

            //update
            var setting = await GetSettingByIdAsync(settingForCaching.Id);
            await DeleteSettingAsync(setting);
        }

        ///// <summary>
        ///// Clear cache
        ///// </summary>
        ///// <returns>A task that represents the asynchronous operation</returns>
        //public virtual async Task ClearCacheAsync()
        //{
        //    await _staticCacheManager.RemoveByPrefixAsync(NopEntityCacheDefaults<Setting>.Prefix);
        //}

        /// <summary>
        /// Get setting key (stored into database)
        /// </summary>
        /// <typeparam name="TSettings">Type of settings</typeparam>
        /// <typeparam name="T">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <returns>Key</returns>
        public virtual string GetSettingKey<TSettings, T>(TSettings settings, Expression<Func<TSettings, T>> keySelector)
            where TSettings : ISettings, new()
        {
            if (keySelector.Body is not MemberExpression member)
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");

            if (member.Member is not PropertyInfo propInfo)
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");

            var key = $"{typeof(TSettings).Name}.{propInfo.Name}";

            return key;
        }

    }
}
