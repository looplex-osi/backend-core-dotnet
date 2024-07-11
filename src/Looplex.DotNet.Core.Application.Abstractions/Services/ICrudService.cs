using Looplex.OpenForExtension.Context;
using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Services
{
    public interface ICrudService
    {
        Task GetAllAsync(IDefaultContext context);
        Task GetByIdAsync(IDefaultContext context);
        Task CreateAsync(IDefaultContext context);
        Task DeleteAsync(IDefaultContext context);
    }
}
