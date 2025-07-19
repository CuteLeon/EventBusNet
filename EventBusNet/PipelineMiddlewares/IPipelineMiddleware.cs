namespace EventBusNet.PipelineMiddlewares;

public interface IPipelineMiddleware
{
    void Process(EventBase @event);
}
