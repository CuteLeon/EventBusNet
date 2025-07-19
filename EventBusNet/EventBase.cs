namespace EventBusNet;

public abstract record EventBase
{
    private static long currentEventId = 0;

    public long EventId { get; init; }

    public long? ParentEventId { get; set; }

    public EventBase()
    {
        this.EventId = Interlocked.Increment(ref currentEventId);
    }
}
