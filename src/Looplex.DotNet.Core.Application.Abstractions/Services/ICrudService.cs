using System.Threading.Tasks;
using Looplex.OpenForExtension.Abstractions.Contexts;

namespace Looplex.DotNet.Core.Application.Abstractions.Services
{
    public interface ICrudService
    {
        Task GetAllAsync(IContext context);
        Task GetByIdAsync(IContext context);
        Task CreateAsync(IContext context);
        Task UpdateAsync(IContext context);
        Task PatchAsync(IContext context);
        Task DeleteAsync(IContext context);
    }
}
