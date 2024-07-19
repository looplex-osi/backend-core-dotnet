using System.Threading;
using Looplex.OpenForExtension.Context;
using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Services
{
    public interface ICrudService
    {
        Task GetAllAsync(IDefaultContext context, CancellationToken cancellationToken);
        Task GetByIdAsync(IDefaultContext context, CancellationToken cancellationToken);
        Task CreateAsync(IDefaultContext context, CancellationToken cancellationToken);
        Task DeleteAsync(IDefaultContext context, CancellationToken cancellationToken);
    }
}
