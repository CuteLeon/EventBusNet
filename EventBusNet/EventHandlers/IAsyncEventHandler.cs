namespace EventBusNet.EventHandlers;

public interface IAsyncEventHandler<in TEvent>
    where TEvent : EventBase
{
    Task Handle(TEvent @event);
}
