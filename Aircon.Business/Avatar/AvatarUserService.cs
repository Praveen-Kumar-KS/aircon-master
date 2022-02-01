using Aircon.Core.Caching;
using Aircon.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Avatar
{
    public interface IAvatarUserService
    {
        Task<AvatarUserModel> GetUser(int id);
    }
    public class AvatarUserService : IAvatarUserService
    {
        private readonly AirconDbContext _airconDbContext;
        private readonly IStaticCacheManager _staticCacheManager;

        public AvatarUserService(AirconDbContext airconDbContext,
            IStaticCacheManager staticCacheManager)
        {
            _airconDbContext = airconDbContext;
            _staticCacheManager = staticCacheManager;
        }


        public static CacheKey AvatarUserServiceCacheKey => new CacheKey("AirCon.AvatarUserService.{0}");

        public async Task<AvatarUserModel> GetUser(int userId)
        {
            var key = _staticCacheManager.PrepareKeyForShortTermCache(AvatarUserServiceCacheKey, userId);
            var avatarUserModel = await _staticCacheManager.GetAsync(key, async () => {
                var query = _airconDbContext.Users
                    .Where(x => x.Id == userId)
                    .Select(x => new AvatarUserModel
                    {
                        UserId = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        WorkTitle = x.WorkTitle,
                        AvatarId = x.AvatarId
                    }).AsQueryable();
                return await query.SingleOrDefaultAsync();
            });
            return avatarUserModel;
        }

    }
}
