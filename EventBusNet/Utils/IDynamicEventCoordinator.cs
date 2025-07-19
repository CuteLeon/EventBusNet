namespace EventBusNet.Utils;

public interface IDynamicEventCoordinator
{
    void Subscribe<TEvent>(EventHandler<TEvent> eventHandler)
        where TEvent : EventBase;

    void Unsubscribe<TEvent>(EventHandler<TEvent> eventHandler)
        where TEvent : EventBase;
}
