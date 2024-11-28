using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Services;

public interface IRedisService
{
    Task SetAsync(string key, string value);
    Task<string?> GetAsync(string key);
    Task<bool> DeleteAsync(string key);
}