using Looplex.DotNet.Core.Application.Abstractions.Services;

namespace Looplex.DotNet.Core.Application.Abstractions.Factories;

public interface ICacheServiceFactory
{
    void RegisterCacheService(string name, ICacheService cacheService);
    ICacheService GetCacheService(string name);
}