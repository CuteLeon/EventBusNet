using System.Diagnostics;

namespace EventBusNet.EventHandlers;

[DebuggerStepThrough]
public class DefaultDynamicAsyncEventHandler<TEvent> : IDynamicAsyncEventHandler<TEvent>
    where TEvent : EventBase
{
    protected object _lock = new();

    protected event EventHandler<TEvent>? Event;

    public void Subscribe(EventHandler<TEvent> eventHandler)
    {
        lock (this._lock)
            this.Event += eventHandler;
    }

    public void Unsubscribe(EventHandler<TEvent> eventHandler)
    {
        lock (this._lock)
            this.Event -= eventHandler;
    }

    public Task Handle(TEvent @event)
    {
        this.Event?.Invoke(this, @event);
        return Task.CompletedTask;
    }
}
