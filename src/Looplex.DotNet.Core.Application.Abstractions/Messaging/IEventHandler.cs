using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Messaging
{
    public interface IEventHandler<T> where T : class, IEvent
    {
        Task HandleAsync(T eventToHandle);
    }
}
