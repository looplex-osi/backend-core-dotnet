using System;
using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Services;

public interface ICacheService
{
    Task SetCacheValueAsync(string key, string value, TimeSpan? expiry = null);
    Task<bool> ContainsKeyAsync(string key);
    Task<bool> TryGetCacheValueAsync(string key, out string value);
    Task RemoveCacheValueAsync(string key);
}