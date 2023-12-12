using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace MessagingForFun.Infrastructure;

public static class CacheHelper
{
    public static async Task SetRecordAsync<T>(this IDistributedCache cache,
        string recordId,
        T data,
        TimeSpan? absoluteExpireTime = null,
        TimeSpan? unusedExpireTime = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
            SlidingExpiration = unusedExpireTime
        };

        var jsonData = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(recordId, jsonData, options);
    }
    
    public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache, string recordId) =>
        await cache.GetStringAsync(recordId) is { } jsonData
            ? JsonSerializer.Deserialize<T>(jsonData)
            : default;
    
}