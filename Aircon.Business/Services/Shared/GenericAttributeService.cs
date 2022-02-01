using Aircon.Business.Models.Shared;
using Aircon.Core;
using Aircon.Core.Caching;
using Aircon.Core.Data;
using Aircon.Data;
using Aircon.Data.Entities;
//using Aircon.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Services.Shared
{

    public partial interface IGenericAttributeService
    {
        /// <summary>
        /// Deletes an attribute
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteAttributeAsync(GenericAttributeModel attribute);

        /// <summary>
        /// Deletes an attributes
        /// </summary>
        /// <param name="attributes">Attributes</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteAttributesAsync(IList<GenericAttributeModel> attributes);

        /// <summary>
        /// Inserts an attribute
        /// </summary>
        /// <param name="attribute">attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertAttributeAsync(GenericAttributeModel attribute);

        /// <summary>
        /// Updates the attribute
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateAttributeAsync(GenericAttributeModel attribute);

        /// <summary>
        /// Get attributes
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="keyGroup">Key group</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the get attributes
        /// </returns>
        Task<IList<GenericAttributeModel>> GetAttributesForEntityAsync(int entityId, string keyGroup);

        /// <summary>
        /// Save attribute value
        /// </summary>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task SaveAttributeAsync<TPropType>(BaseModel entity, string key, TPropType value);

        /// <summary>
        /// Get an attribute of an entity
        /// </summary>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the attribute
        /// </returns>
        Task<TPropType> GetAttributeAsync<TPropType>(BaseModel entity, string key, TPropType defaultValue = default);

        /// <summary>
        /// Get an attribute of an entity
        /// </summary>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the attribute
        /// </returns>
        Task<TPropType> GetAttributeAsync<TEntity, TPropType>(int entityId, string key, TPropType defaultValue = default)
            where TEntity : BaseModel;
    }


    public partial class GenericAttributeService : IGenericAttributeService
    {
        #region Fields

        private readonly AirconDbContext _airconDbContext;
        private readonly IStaticCacheManager _staticCacheManager;

        #endregion

        #region Ctor

        public GenericAttributeService(AirconDbContext airconDbContext,
            IStaticCacheManager staticCacheManager)
        {
            _airconDbContext = airconDbContext;
            _staticCacheManager = staticCacheManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes an attribute
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAttributeAsync(GenericAttributeModel attribute)
        {
            var entity = await _airconDbContext.GenericAttributes.Where(x => x.Id == attribute.Id).SingleOrDefaultAsync();
            if (entity != null)
            {
                _airconDbContext.GenericAttributes.Remove(entity);
                await _airconDbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes an attributes
        /// </summary>
        /// <param name="attributes">Attributes</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteAttributesAsync(IList<GenericAttributeModel> attributes)
        {
          
            foreach(var attribute in attributes)
            {
                await DeleteAttributeAsync(attribute);
            }
        }

        /// <summary>
        /// Inserts an attribute
        /// </summary>
        /// <param name="attribute">attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertAttributeAsync(GenericAttributeModel attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));
            var entity = new GenericAttribute
            {
                EntityId = attribute.EntityId,
                KeyGroup = attribute.KeyGroup,
                Key = attribute.Key,
                Value = attribute.Value
            };
            _airconDbContext.GenericAttributes.Add(entity);
            await _airconDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the attribute
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateAttributeAsync(GenericAttributeModel attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));

            var entity = await _airconDbContext.GenericAttributes.Where(x => x.Id == attribute.Id).SingleOrDefaultAsync();
            if (entity != null)
            {
                entity.Value = attribute.Value;
                _airconDbContext.GenericAttributes.Update(entity);
                await _airconDbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get attributes
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="keyGroup">Key group</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the get attributes
        /// </returns>
        /// 
        public static CacheKey GenericAttributeCacheKey => new CacheKey("AirCon.genericattribute.{0}-{1}");

        public virtual async Task<IList<GenericAttributeModel>> GetAttributesForEntityAsync(int entityId, string keyGroup)
        {
            var key = _staticCacheManager.PrepareKeyForShortTermCache(GenericAttributeCacheKey, entityId, keyGroup);

            //var query = from ga in _genericAttributeRepository.Table
            //            where ga.EntityId == entityId &&
            //                  ga.KeyGroup == keyGroup
            //            select ga;
            var query = _airconDbContext.GenericAttributes
                .Where(x => x.EntityId == entityId)
                .Where(x => x.KeyGroup == keyGroup)
                .Select(x=> new GenericAttributeModel {
                    Id = x.Id,
                    Key = x.Key,
                    KeyGroup = x.KeyGroup,
                    Value = x.Value,
                    EntityId = x.EntityId
                }).AsQueryable();

            var attributes = await _staticCacheManager.GetAsync(key, async () => await query.ToListAsync());

            return attributes;
        }

        public virtual async Task ClearCacheAttributesForEntityAsync(int entityId, string keyGroup)
        {
            var key = _staticCacheManager.PrepareKeyForShortTermCache(GenericAttributeCacheKey, entityId, keyGroup);

            await _staticCacheManager.RemoveAsync(key);
            //var query = _airconDbContext.GenericAttributes
            //    .Where(x => x.EntityId == entityId)
            //    .Where(x => x.KeyGroup == keyGroup)
            //    .Select(x => new GenericAttributeModel
            //    {
            //        Id = x.Id,
            //        Key = x.Key,
            //        KeyGroup = x.KeyGroup,
            //        Value = x.Value,
            //        EntityId = x.EntityId
            //    }).AsQueryable();

            //var attributes = await _staticCacheManager.GetAsync(key, async () => await query.ToListAsync());

        }

        /// <summary>
        /// Save attribute value
        /// </summary>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task SaveAttributeAsync<TPropType>(BaseModel entity, string key, TPropType value)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var keyGroup = entity.GetType().Name;

            var props = (await GetAttributesForEntityAsync(entity.Id, keyGroup))
                .ToList();
            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            var valueStr = CommonHelper.To<string>(value);

            if (prop != null)
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                    //delete
                    await DeleteAttributeAsync(prop);
                else
                {
                    //update
                    prop.Value = valueStr;
                    await UpdateAttributeAsync(prop);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                    return;

                //insert
                prop = new GenericAttributeModel
                {
                    EntityId = entity.Id,
                    Key = key,
                    KeyGroup = keyGroup,
                    Value = valueStr,
                };

                await InsertAttributeAsync(prop);
            }
            await ClearCacheAttributesForEntityAsync(entity.Id, keyGroup);
        }

        /// <summary>
        /// Get an attribute of an entity
        /// </summary>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the attribute
        /// </returns>
        public virtual async Task<TPropType> GetAttributeAsync<TPropType>(BaseModel entity, string key, TPropType defaultValue = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var keyGroup = entity.GetType().Name;

            var props = await GetAttributesForEntityAsync(entity.Id, keyGroup);

            //little hack here (only for unit testing). we should write expect-return rules in unit tests for such cases
            if (props == null)
                return defaultValue;

            //props = props.ToList();
            if (!props.Any())
                return defaultValue;

            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            if (prop == null || string.IsNullOrEmpty(prop.Value))
                return defaultValue;

            return CommonHelper.To<TPropType>(prop.Value);
        }

        /// <summary>
        /// Get an attribute of an entity
        /// </summary>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the attribute
        /// </returns>
        public virtual async Task<TPropType> GetAttributeAsync<TEntity, TPropType>(int entityId, string key, TPropType defaultValue = default)
            where TEntity : BaseModel
        {
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
            entity.Id = entityId;

            return await GetAttributeAsync(entity, key, defaultValue);
        }

        #endregion
    }

}
