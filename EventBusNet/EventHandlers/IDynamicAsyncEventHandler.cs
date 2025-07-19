namespace EventBusNet.EventHandlers;

public interface IDynamicAsyncEventHandler<TEvent> : IAsyncEventHandler<TEvent>
    where TEvent : EventBase
{
    void Subscribe(EventHandler<TEvent> eventHandler);

    void Unsubscribe(EventHandler<TEvent> eventHandler);
}
