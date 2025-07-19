namespace EventBusNet.Raisers;

public interface IEventRaiser
{
    Task Raise<TEvent>(TEvent @event)
        where TEvent : EventBase;
}
