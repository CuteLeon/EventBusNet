using EventBusNet.PipelineMiddlewares;
using EventBusNet.Resolvers;

namespace EventBusNet.Raisers;

public class DefaultEventRaiser(
    IEnumerable<IPipelineMiddleware> pipelineMiddlewares,
    IAsyncEventHandlerResolver asyncEventHandlerResolver) : IEventRaiser
{
    private readonly IPipelineMiddleware[] pipelineMiddlewares = [.. pipelineMiddlewares];
    private readonly IAsyncEventHandlerResolver asyncEventHandlerResolver = asyncEventHandlerResolver;

    public Task Raise<TEvent>(TEvent @event) where TEvent : EventBase
    {
        if (this.pipelineMiddlewares.Length > 0)
        {
            foreach (var middleware in this.pipelineMiddlewares)
                middleware.Process(@event);
        }

        var eventPipelineMiddlewares = this.asyncEventHandlerResolver.ResolveEventPipelineMiddlewares<TEvent>();
        if (eventPipelineMiddlewares is not null && eventPipelineMiddlewares.Count != 0)
        {
            foreach (var middleware in eventPipelineMiddlewares)
                middleware.Process(@event);
        }

        var eventHandlers = this.asyncEventHandlerResolver.ResolveAsyncEventHandlers<TEvent>();
        if (eventHandlers is null) return Task.CompletedTask;

        // https://github.com/dotnet/runtime/pull/117715
        return eventHandlers.Count switch
        {
            0 => Task.CompletedTask,
            1 => eventHandlers.First().Handle(@event),
            _ => Task.WhenAll(eventHandlers.Select(handler => handler.Handle(@event)).ToArray()),
        };
    }
}
