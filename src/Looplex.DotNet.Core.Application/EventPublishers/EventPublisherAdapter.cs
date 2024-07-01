using Looplex.DotNet.Core.Application.Abstractions.Messaging;
using MassTransit;

namespace Looplex.DotNet.Core.Application.EventPublishers
{
    public class EventPublisherAdapter<T> : IEventPublisher<T> where T : class, IEvent
    {
        private readonly IBus _bus;

        public EventPublisherAdapter(IBus bus)
        {
            _bus = bus;
        }

        public virtual Task PublishAsync(T eventToPublish)
        {
            return _bus.Publish(eventToPublish);
        }
    }
}
