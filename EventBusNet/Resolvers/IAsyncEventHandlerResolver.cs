using EventBusNet.EventHandlers;
using EventBusNet.PipelineMiddlewares;

namespace EventBusNet.Resolvers;

public interface IAsyncEventHandlerResolver
{
    IReadOnlyCollection<IEventPipelineMiddleware<TEvent>> ResolveEventPipelineMiddlewares<TEvent>()
        where TEvent : EventBase;

    IReadOnlyCollection<IAsyncEventHandler<TEvent>> ResolveAsyncEventHandlers<TEvent>()
        where TEvent : EventBase;

    IDynamicAsyncEventHandler<TEvent> ResolveDynamicEventHandler<TEvent>()
        where TEvent : EventBase;
}
