using EventBusNet;
using EventBusNet.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusNet.Extensions;

public static class RegisterEventExtensions
{
    private static IServiceCollection AddEventCore<TEvent, TService>(this IServiceCollection services)
        where TEvent : EventBase
        where TService : class, IAsyncEventHandler<TEvent>
        => services.AddSingleton<IAsyncEventHandler<TEvent>>(sp => sp.GetRequiredService<TService>());

    public static IServiceCollection AddEvent<TEvent, TService1>(this IServiceCollection services)
        where TEvent : EventBase
        where TService1 : class, IAsyncEventHandler<TEvent>
        => services
            .AddEventCore<TEvent, TService1>();

    public static IServiceCollection AddEvent<TEvent, TService1, TService2>(this IServiceCollection services)
        where TEvent : EventBase
        where TService1 : class, IAsyncEventHandler<TEvent>
        where TService2 : class, IAsyncEventHandler<TEvent>
        => services
            .AddEventCore<TEvent, TService1>()
            .AddEventCore<TEvent, TService2>();

    public static IServiceCollection AddEvent<TEvent, TService1, TService2, TService3>(this IServiceCollection services)
        where TEvent : EventBase
        where TService1 : class, IAsyncEventHandler<TEvent>
        where TService2 : class, IAsyncEventHandler<TEvent>
        where TService3 : class, IAsyncEventHandler<TEvent>
        => services
            .AddEventCore<TEvent, TService1>()
            .AddEventCore<TEvent, TService2>()
            .AddEventCore<TEvent, TService3>();

    public static IServiceCollection AddEvent<TEvent, TService1, TService2, TService3, TService4>(this IServiceCollection services)
        where TEvent : EventBase
        where TService1 : class, IAsyncEventHandler<TEvent>
        where TService2 : class, IAsyncEventHandler<TEvent>
        where TService3 : class, IAsyncEventHandler<TEvent>
        where TService4 : class, IAsyncEventHandler<TEvent>
        => services
            .AddEventCore<TEvent, TService1>()
            .AddEventCore<TEvent, TService2>()
            .AddEventCore<TEvent, TService3>()
            .AddEventCore<TEvent, TService4>();

    public static IServiceCollection AddEvent<TEvent, TService1, TService2, TService3, TService4, TService5>(this IServiceCollection services)
        where TEvent : EventBase
        where TService1 : class, IAsyncEventHandler<TEvent>
        where TService2 : class, IAsyncEventHandler<TEvent>
        where TService3 : class, IAsyncEventHandler<TEvent>
        where TService4 : class, IAsyncEventHandler<TEvent>
        where TService5 : class, IAsyncEventHandler<TEvent>
        => services
            .AddEventCore<TEvent, TService1>()
            .AddEventCore<TEvent, TService2>()
            .AddEventCore<TEvent, TService3>()
            .AddEventCore<TEvent, TService4>()
            .AddEventCore<TEvent, TService5>();

    public static IServiceCollection AddEvent<TEvent, TService1, TService2, TService3, TService4, TService5, TService6>(this IServiceCollection services)
        where TEvent : EventBase
        where TService1 : class, IAsyncEventHandler<TEvent>
        where TService2 : class, IAsyncEventHandler<TEvent>
        where TService3 : class, IAsyncEventHandler<TEvent>
        where TService4 : class, IAsyncEventHandler<TEvent>
        where TService5 : class, IAsyncEventHandler<TEvent>
        where TService6 : class, IAsyncEventHandler<TEvent>
        => services
            .AddEventCore<TEvent, TService1>()
            .AddEventCore<TEvent, TService2>()
            .AddEventCore<TEvent, TService3>()
            .AddEventCore<TEvent, TService4>()
            .AddEventCore<TEvent, TService5>()
            .AddEventCore<TEvent, TService6>();

    public static IServiceCollection AddEvent<TEvent, TService1, TService2, TService3, TService4, TService5, TService6, TService7>(this IServiceCollection services)
        where TEvent : EventBase
        where TService1 : class, IAsyncEventHandler<TEvent>
        where TService2 : class, IAsyncEventHandler<TEvent>
        where TService3 : class, IAsyncEventHandler<TEvent>
        where TService4 : class, IAsyncEventHandler<TEvent>
        where TService5 : class, IAsyncEventHandler<TEvent>
        where TService6 : class, IAsyncEventHandler<TEvent>
        where TService7 : class, IAsyncEventHandler<TEvent>
        => services
            .AddEventCore<TEvent, TService1>()
            .AddEventCore<TEvent, TService2>()
            .AddEventCore<TEvent, TService3>()
            .AddEventCore<TEvent, TService4>()
            .AddEventCore<TEvent, TService5>()
            .AddEventCore<TEvent, TService6>()
            .AddEventCore<TEvent, TService7>();

    public static IServiceCollection AddEvent<TEvent, TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8>(this IServiceCollection services)
        where TEvent : EventBase
        where TService1 : class, IAsyncEventHandler<TEvent>
        where TService2 : class, IAsyncEventHandler<TEvent>
        where TService3 : class, IAsyncEventHandler<TEvent>
        where TService4 : class, IAsyncEventHandler<TEvent>
        where TService5 : class, IAsyncEventHandler<TEvent>
        where TService6 : class, IAsyncEventHandler<TEvent>
        where TService7 : class, IAsyncEventHandler<TEvent>
        where TService8 : class, IAsyncEventHandler<TEvent>
        => services
            .AddEventCore<TEvent, TService1>()
            .AddEventCore<TEvent, TService2>()
            .AddEventCore<TEvent, TService3>()
            .AddEventCore<TEvent, TService4>()
            .AddEventCore<TEvent, TService5>()
            .AddEventCore<TEvent, TService6>()
            .AddEventCore<TEvent, TService7>()
            .AddEventCore<TEvent, TService8>();

    public static IServiceCollection SubscribeEvent<TService, TEvent1>(this IServiceCollection services)
        where TService : class, IAsyncEventHandler<TEvent1>
        where TEvent1 : EventBase
        => services
            .AddEventCore<TEvent1, TService>();

    public static IServiceCollection SubscribeEvent<TService, TEvent1, TEvent2>(this IServiceCollection services)
        where TService : class, IAsyncEventHandler<TEvent1>, IAsyncEventHandler<TEvent2>
        where TEvent1 : EventBase
        where TEvent2 : EventBase
        => services
            .AddEventCore<TEvent1, TService>()
            .AddEventCore<TEvent2, TService>();

    public static IServiceCollection SubscribeEvent<TService, TEvent1, TEvent2, TEvent3>(this IServiceCollection services)
        where TService : class, IAsyncEventHandler<TEvent1>, IAsyncEventHandler<TEvent2>, IAsyncEventHandler<TEvent3>
        where TEvent1 : EventBase
        where TEvent2 : EventBase
        where TEvent3 : EventBase
        => services
            .AddEventCore<TEvent1, TService>()
            .AddEventCore<TEvent2, TService>()
            .AddEventCore<TEvent3, TService>();

    public static IServiceCollection SubscribeEvent<TService, TEvent1, TEvent2, TEvent3, TEvent4>(this IServiceCollection services)
        where TService : class, IAsyncEventHandler<TEvent1>, IAsyncEventHandler<TEvent2>, IAsyncEventHandler<TEvent3>, IAsyncEventHandler<TEvent4>
        where TEvent1 : EventBase
        where TEvent2 : EventBase
        where TEvent3 : EventBase
        where TEvent4 : EventBase
        => services
            .AddEventCore<TEvent1, TService>()
            .AddEventCore<TEvent2, TService>()
            .AddEventCore<TEvent3, TService>()
            .AddEventCore<TEvent4, TService>();

    public static IServiceCollection SubscribeEvent<TService, TEvent1, TEvent2, TEvent3, TEvent4, TEvent5>(this IServiceCollection services)
        where TService : class, IAsyncEventHandler<TEvent1>, IAsyncEventHandler<TEvent2>, IAsyncEventHandler<TEvent3>, IAsyncEventHandler<TEvent4>, IAsyncEventHandler<TEvent5>
        where TEvent1 : EventBase
        where TEvent2 : EventBase
        where TEvent3 : EventBase
        where TEvent4 : EventBase
        where TEvent5 : EventBase
        => services
            .AddEventCore<TEvent1, TService>()
            .AddEventCore<TEvent2, TService>()
            .AddEventCore<TEvent3, TService>()
            .AddEventCore<TEvent4, TService>()
            .AddEventCore<TEvent5, TService>();

    public static IServiceCollection SubscribeEvent<TService, TEvent1, TEvent2, TEvent3, TEvent4, TEvent5, TEvent6>(this IServiceCollection services)
        where TService : class, IAsyncEventHandler<TEvent1>, IAsyncEventHandler<TEvent2>, IAsyncEventHandler<TEvent3>, IAsyncEventHandler<TEvent4>, IAsyncEventHandler<TEvent5>, IAsyncEventHandler<TEvent6>
        where TEvent1 : EventBase
        where TEvent2 : EventBase
        where TEvent3 : EventBase
        where TEvent4 : EventBase
        where TEvent5 : EventBase
        where TEvent6 : EventBase
        => services
            .AddEventCore<TEvent1, TService>()
            .AddEventCore<TEvent2, TService>()
            .AddEventCore<TEvent3, TService>()
            .AddEventCore<TEvent4, TService>()
            .AddEventCore<TEvent5, TService>()
            .AddEventCore<TEvent6, TService>();

    public static IServiceCollection SubscribeEvent<TService, TEvent1, TEvent2, TEvent3, TEvent4, TEvent5, TEvent6, TEvent7>(this IServiceCollection services)
        where TService : class, IAsyncEventHandler<TEvent1>, IAsyncEventHandler<TEvent2>, IAsyncEventHandler<TEvent3>, IAsyncEventHandler<TEvent4>, IAsyncEventHandler<TEvent5>, IAsyncEventHandler<TEvent6>, IAsyncEventHandler<TEvent7>
        where TEvent1 : EventBase
        where TEvent2 : EventBase
        where TEvent3 : EventBase
        where TEvent4 : EventBase
        where TEvent5 : EventBase
        where TEvent6 : EventBase
        where TEvent7 : EventBase
        => services
            .AddEventCore<TEvent1, TService>()
            .AddEventCore<TEvent2, TService>()
            .AddEventCore<TEvent3, TService>()
            .AddEventCore<TEvent4, TService>()
            .AddEventCore<TEvent5, TService>()
            .AddEventCore<TEvent6, TService>()
            .AddEventCore<TEvent7, TService>();

    public static IServiceCollection SubscribeEvent<TService, TEvent1, TEvent2, TEvent3, TEvent4, TEvent5, TEvent6, TEvent7, TEvent8>(this IServiceCollection services)
        where TService : class, IAsyncEventHandler<TEvent1>, IAsyncEventHandler<TEvent2>, IAsyncEventHandler<TEvent3>, IAsyncEventHandler<TEvent4>, IAsyncEventHandler<TEvent5>, IAsyncEventHandler<TEvent6>, IAsyncEventHandler<TEvent7>, IAsyncEventHandler<TEvent8>
        where TEvent1 : EventBase
        where TEvent2 : EventBase
        where TEvent3 : EventBase
        where TEvent4 : EventBase
        where TEvent5 : EventBase
        where TEvent6 : EventBase
        where TEvent7 : EventBase
        where TEvent8 : EventBase
        => services
            .AddEventCore<TEvent1, TService>()
            .AddEventCore<TEvent2, TService>()
            .AddEventCore<TEvent3, TService>()
            .AddEventCore<TEvent4, TService>()
            .AddEventCore<TEvent5, TService>()
            .AddEventCore<TEvent7, TService>()
            .AddEventCore<TEvent8, TService>();
}
