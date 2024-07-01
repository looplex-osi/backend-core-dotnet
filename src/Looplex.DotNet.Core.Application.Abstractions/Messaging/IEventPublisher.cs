using System.Threading.Tasks;

namespace Looplex.DotNet.Core.Application.Abstractions.Messaging
{
    public interface IEventPublisher<T> where T : IEvent
    {
        Task PublishAsync(T eventToPublish);
    }
}
