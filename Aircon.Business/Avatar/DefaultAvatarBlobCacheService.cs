using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Aircon.Business.Avatar
{
    public class DefaultAvatarBlobCacheService : IAvatarBlobCacheService
    {
        private readonly ILogger<DefaultAvatarBlobCacheService> _log;

        public DefaultAvatarBlobCacheService(ILogger<DefaultAvatarBlobCacheService> log)
        {
            _log = log;
        }

        public Task<byte[]> GetBlob(string key, CancellationToken cancellationToken)
        {
            return Task.FromResult((byte[])null);
        }

        public Task StoreBlob(string key, byte[] buffer, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}
