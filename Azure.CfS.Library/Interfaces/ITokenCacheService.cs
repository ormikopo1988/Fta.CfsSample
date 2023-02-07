using System;
using System.Threading.Tasks;

namespace Azure.CfS.Library.Interfaces
{
    public interface ITokenCacheService
    {
        Task CacheTokenAsync(string cacheKey, string token, TimeSpan timeToLive);

        Task<string?> GetCachedTokenAsync(string cacheKey);
    }
}
