using System.Threading;
using System.Threading.Tasks;
using Looplex.OpenForExtension.Abstractions.Contexts;

namespace Looplex.DotNet.Core.Application.Abstractions.Services
{
    public interface ICrudService
    {
        Task GetAllAsync(IContext context, CancellationToken cancellationToken);
        Task GetByIdAsync(IContext context, CancellationToken cancellationToken);
        Task CreateAsync(IContext context, CancellationToken cancellationToken);
        Task UpdateAsync(IContext context, CancellationToken cancellationToken);
        Task PatchAsync(IContext context, CancellationToken cancellationToken);
        Task DeleteAsync(IContext context, CancellationToken cancellationToken);
    }
}
