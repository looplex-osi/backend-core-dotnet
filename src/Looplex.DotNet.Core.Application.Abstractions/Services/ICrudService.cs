using Looplex.OpenForExtension.Context;
using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Services
{
    public interface ICrudService
    {
        Task GetAll(IDefaultContext context);
        Task GetAsync(IDefaultContext context);
        Task CreateAsync(IDefaultContext context);
        Task DeleteAsync(IDefaultContext context);
    }
}
