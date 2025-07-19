using EventBusNet.EventHandlers;
using EventBusNet.PipelineMiddlewares;
using EventBusNet.Raisers;
using EventBusNet.Resolvers;
using EventBusNet.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusNet.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddEventBusNet(this IServiceCollection services, Action<EventBusConfig> optionsAction)
    {
        return services
            .Configure(optionsAction)
            .AddSingleton<IEventBus, DefaultEventBus>()
            .AddEventBusPipelineMiddlewares(optionsAction)
            .AddSingleton<IEventRaiser, DefaultEventRaiser>()
            .AddSingleton<IDynamicEventCoordinator, DefaultDynamicEventCoordinator>()
            .AddSingleton<IAsyncEventHandlerResolver, DefaultAsyncEventHandlerResolver>()
            .AddSingleton(typeof(IDynamicAsyncEventHandler<>), typeof(DefaultDynamicAsyncEventHandler<>));
    }

    private static IServiceCollection AddEventBusPipelineMiddlewares(this IServiceCollection services, Action<EventBusConfig> optionsAction)
    {
        var eventBusOptions = new EventBusConfig();
        optionsAction(eventBusOptions);

        if (eventBusOptions.Logging)
            services.AddSingleton<IPipelineMiddleware, DefaultPipelineLoggingMiddleware>();

        return services;
    }

    public static void ValidateEventBusNet(this IServiceCollection services)
    {
        var eventHandlerInterfaceType = typeof(IAsyncEventHandler<>);
        var eventHandlersHashSet = new HashSet<KeyValuePair<Type, object?>>();
        var invalidServiceDescriptors = services.Where(descriptor =>
        {
            var serviceType = descriptor.ServiceType;
            if (serviceType.IsGenericType &&
                serviceType.GetGenericTypeDefinition() == eventHandlerInterfaceType)
            {
                var pair = KeyValuePair.Create(serviceType, descriptor.ImplementationType ?? descriptor.ImplementationInstance ?? descriptor.ImplementationFactory);
                if (eventHandlersHashSet.Contains(pair)) return true;
                if (descriptor.IsKeyedService) return true;
                if (descriptor.Lifetime != ServiceLifetime.Singleton) return true;
                if (descriptor.ImplementationType is not null) return true;

                eventHandlersHashSet.Add(pair);
            }
            return false;
        }).ToArray();
        if (invalidServiceDescriptors.Length > 0)
            throw new TypeAccessException($"Invalid event handler descriptors:\n\t{string.Join("\n\t", invalidServiceDescriptors.Select(x => $"[{x.Lifetime,-9}] [{(x.IsKeyedService ? "Keyed" : ""),-5}] {x.ServiceType.FullName} => Type:{x.ImplementationType?.FullName}"))}");
    }
}
