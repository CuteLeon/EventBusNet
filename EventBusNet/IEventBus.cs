using EventBusNet.Raisers;
using EventBusNet.Resolvers;
using EventBusNet.Utils;

namespace EventBusNet;

public interface IEventBus : IEventRaiser, IDynamicEventCoordinator, IAsyncEventHandlerResolver
{
}
