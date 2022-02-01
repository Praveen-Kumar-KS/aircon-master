using System.Threading;
using System.Threading.Tasks;

namespace Aircon.Business.Avatar
{

    public interface IAvatarBlobCacheService
    {
        Task<byte[]> GetBlob(string key, CancellationToken cancellationToken = default);
        Task StoreBlob(string key, byte[] buffer, CancellationToken cancellationToken = default);
    }
}
