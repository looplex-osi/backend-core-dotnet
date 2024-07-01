using Looplex.DotNet.Core.Application.Abstractions.Messaging;
using MassTransit;

namespace Looplex.DotNet.Core.Application.EventHandlers
{
    public abstract class EventHandlerAdapter<T> : IEventHandler<T>, IConsumer<T> where T : class, IEvent
    {
        public Task Consume(ConsumeContext<T> context)
        {
            return HandleAsync(context.Message);
        }

        public abstract Task HandleAsync(T eventToHandle);
    }
}
