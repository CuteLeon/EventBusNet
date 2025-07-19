using System.Reflection;
using EventBusNet.Attributes;
using EventBusNet.EventHandlers;
using EventBusNet.PipelineMiddlewares;

namespace EventBusNet.Utils;

public static class EventContext<TEvent> where TEvent : EventBase
{
    static EventContext()
    {
        EventDescription = typeof(TEvent).GetCustomAttribute<EventDescriptionAttribute>(true);
    }

    public static EventDescriptionAttribute? EventDescription { get; internal set; }

    public static IAsyncEventHandler<TEvent>[]? AsyncEventHandlers { get; internal set; }

    public static IDynamicAsyncEventHandler<TEvent>? DynamicAsyncEventHandler { get; internal set; }

    public static IEventPipelineMiddleware<TEvent>[]? EventPipelineMiddlewares { get; internal set; }
}
