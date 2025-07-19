using System.Diagnostics;
using EventBusNet.EventHandlers;
using EventBusNet.PipelineMiddlewares;
using EventBusNet.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusNet.Resolvers;

[DebuggerStepThrough]
public class DefaultAsyncEventHandlerResolver(IServiceProvider serviceProvider) : IAsyncEventHandlerResolver
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public IReadOnlyCollection<IAsyncEventHandler<TEvent>> ResolveAsyncEventHandlers<TEvent>() where TEvent : EventBase
    {
        return EventContext<TEvent>.AsyncEventHandlers ??=
            [.. this.serviceProvider.GetServices<IAsyncEventHandler<TEvent>>().Append(this.ResolveDynamicEventHandler<TEvent>()).Distinct()];
    }

    public IDynamicAsyncEventHandler<TEvent> ResolveDynamicEventHandler<TEvent>() where TEvent : EventBase
    {
        return EventContext<TEvent>.DynamicAsyncEventHandler ??= this.serviceProvider.GetRequiredService<IDynamicAsyncEventHandler<TEvent>>();
    }

    public IReadOnlyCollection<IEventPipelineMiddleware<TEvent>> ResolveEventPipelineMiddlewares<TEvent>() where TEvent : EventBase
    {
        return EventContext<TEvent>.EventPipelineMiddlewares ??=
            [.. this.serviceProvider.GetServices<IEventPipelineMiddleware<TEvent>>().Distinct()];
    }
}
