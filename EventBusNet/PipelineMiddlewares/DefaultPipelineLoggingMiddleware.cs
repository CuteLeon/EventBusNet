using System.Diagnostics;

namespace EventBusNet.PipelineMiddlewares;

[DebuggerStepThrough]
public class DefaultPipelineLoggingMiddleware : IPipelineMiddleware
{
    public void Process(EventBase @event)
    {
    }
}
