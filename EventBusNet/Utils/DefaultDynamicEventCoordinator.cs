using System.Diagnostics;
using EventBusNet.Resolvers;

namespace EventBusNet.Utils;

[DebuggerStepThrough]
public class DefaultDynamicEventCoordinator(IAsyncEventHandlerResolver eventHandlerResolver) : IDynamicEventCoordinator
{
    private readonly IAsyncEventHandlerResolver eventHandlerResolver = eventHandlerResolver;

    public void Subscribe<TEvent>(EventHandler<TEvent> eventHandler) where TEvent : EventBase
    {
        var dynamicEventHandler = this.eventHandlerResolver.ResolveDynamicEventHandler<TEvent>();
        dynamicEventHandler?.Subscribe(eventHandler);
    }

    public void Unsubscribe<TEvent>(EventHandler<TEvent> eventHandler) where TEvent : EventBase
    {
        var dynamicEventHandler = this.eventHandlerResolver.ResolveDynamicEventHandler<TEvent>();
        dynamicEventHandler?.Unsubscribe(eventHandler);
    }
}
