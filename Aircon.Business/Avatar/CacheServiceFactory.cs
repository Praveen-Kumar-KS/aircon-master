using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Aircon.Business.Avatar
{
    public static class CacheServiceFactory
    {
        public static IAvatarBlobCacheService CreateInstance(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            if (!string.IsNullOrWhiteSpace(configuration["FileCache:Path"]))
            {
                return new FileSystemAvatarBlobCacheService(configuration, loggerFactory.CreateLogger<FileSystemAvatarBlobCacheService>());
            }

            if (!string.IsNullOrWhiteSpace(configuration["AzureStorage:ConnectionString"]))
            {
                return new AzureBlobCacherService(configuration, loggerFactory.CreateLogger<AzureBlobCacherService>());
            }

            return new DefaultAvatarBlobCacheService(loggerFactory.CreateLogger<DefaultAvatarBlobCacheService>());
        }
    }
}
