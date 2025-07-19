using System.Diagnostics;
using EventBusNet.EventHandlers;
using EventBusNet.PipelineMiddlewares;
using EventBusNet.Raisers;
using EventBusNet.Resolvers;
using EventBusNet.Utils;

namespace EventBusNet;

[DebuggerStepThrough]
public class DefaultEventBus(
    IEventRaiser eventRaiser,
    IAsyncEventHandlerResolver asyncEventHandlerResolver,
    IDynamicEventCoordinator dynamicEventCoordinator) : IEventBus
{
    private readonly IEventRaiser eventRaiser = eventRaiser;
    private readonly IAsyncEventHandlerResolver asyncEventHandlerResolver = asyncEventHandlerResolver;
    private readonly IDynamicEventCoordinator dynamicEventCoordinator = dynamicEventCoordinator;

    public Task Raise<TEvent>(TEvent @event) where TEvent : EventBase
    {
        return this.eventRaiser.Raise(@event);
    }

    public IReadOnlyCollection<IAsyncEventHandler<TEvent>> ResolveAsyncEventHandlers<TEvent>() where TEvent : EventBase
    {
        return this.asyncEventHandlerResolver.ResolveAsyncEventHandlers<TEvent>();
    }

    public IDynamicAsyncEventHandler<TEvent> ResolveDynamicEventHandler<TEvent>() where TEvent : EventBase
    {
        return this.asyncEventHandlerResolver.ResolveDynamicEventHandler<TEvent>();
    }

    public IReadOnlyCollection<IEventPipelineMiddleware<TEvent>> ResolveEventPipelineMiddlewares<TEvent>() where TEvent : EventBase
    {
        return this.asyncEventHandlerResolver.ResolveEventPipelineMiddlewares<TEvent>();
    }

    public void Subscribe<TEvent>(EventHandler<TEvent> eventHandler) where TEvent : EventBase
    {
        this.dynamicEventCoordinator.Subscribe(eventHandler);
    }

    public void Unsubscribe<TEvent>(EventHandler<TEvent> eventHandler) where TEvent : EventBase
    {
        this.dynamicEventCoordinator.Unsubscribe(eventHandler);
    }
}
