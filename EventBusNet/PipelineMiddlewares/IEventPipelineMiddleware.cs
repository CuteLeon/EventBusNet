namespace EventBusNet.PipelineMiddlewares;

public interface IEventPipelineMiddleware<TEvent>
    where TEvent : EventBase
{
    void Process(TEvent @event);
}
