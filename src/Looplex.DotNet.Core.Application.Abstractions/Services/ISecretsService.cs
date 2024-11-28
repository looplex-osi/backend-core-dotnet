using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Services;

public interface ISecretsService
{
    Task<string?> GetSecretAsync(string secretName);
}