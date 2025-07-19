using BenchmarkDotNet.Attributes;
using EventBusNet.EventHandlers;
using EventBusNet.Extensions;
using EventBusNet.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusNet.Benchmark;

public class Benchmark_EventBusNet
{
    IServiceProvider serviceProvider = null!;
    IEventBus eventBus = null!;
    Dynamic1Component dynamic1Component = null!;
    Dynamic5Component1 dynamic5Component1 = null!;
    Dynamic5Component2 dynamic5Component2 = null!;
    Dynamic5Component3 dynamic5Component3 = null!;
    Dynamic5Component4 dynamic5Component4 = null!;
    Dynamic5Component5 dynamic5Component5 = null!;
    Dynamic5WorkloadComponent1 dynamic5WorkloadComponent1 = null!;
    Dynamic5WorkloadComponent2 dynamic5WorkloadComponent2 = null!;
    Dynamic5WorkloadComponent3 dynamic5WorkloadComponent3 = null!;
    Dynamic5WorkloadComponent4 dynamic5WorkloadComponent4 = null!;
    Dynamic5WorkloadComponent5 dynamic5WorkloadComponent5 = null!;

    [GlobalSetup(Targets = [
        nameof(RaiseDynamic1Event), nameof(RaiseTrading1Event),
        nameof(RaiseDynamic5Event), nameof(RaiseTrading5Event),
        nameof(RaiseDynamic5WorkloadEvent), nameof(RaiseTrading5WorkloadEvent)])]
    public void GlobalSetup()
    {
        serviceProvider = new ServiceCollection()
            .AddEventBusNet(options => { })
            .AddSingleton<Dynamic1Component>()
            .AddSingleton<Dynamic5Component1>()
            .AddSingleton<Dynamic5Component2>()
            .AddSingleton<Dynamic5Component3>()
            .AddSingleton<Dynamic5Component4>()
            .AddSingleton<Dynamic5Component5>()
            .AddSingleton<Trading5EventHandler1>()
            .AddSingleton<Trading5EventHandler2>()
            .AddSingleton<Trading5EventHandler3>()
            .AddSingleton<Trading5EventHandler4>()
            .AddSingleton<Trading5EventHandler5>()
            .AddEvent<Trading5Event,
                Trading5EventHandler1,
                Trading5EventHandler2,
                Trading5EventHandler3,
                Trading5EventHandler4,
                Trading5EventHandler5>()
            .AddSingleton<Dynamic5WorkloadComponent1>()
            .AddSingleton<Dynamic5WorkloadComponent2>()
            .AddSingleton<Dynamic5WorkloadComponent3>()
            .AddSingleton<Dynamic5WorkloadComponent4>()
            .AddSingleton<Dynamic5WorkloadComponent5>()
            .AddSingleton<Trading5WorkloadEventHandler1>()
            .AddSingleton<Trading5WorkloadEventHandler2>()
            .AddSingleton<Trading5WorkloadEventHandler3>()
            .AddSingleton<Trading5WorkloadEventHandler4>()
            .AddSingleton<Trading5WorkloadEventHandler5>()
            .AddEvent<Trading5WorkloadEvent,
                Trading5WorkloadEventHandler1,
                Trading5WorkloadEventHandler2,
                Trading5WorkloadEventHandler3,
                Trading5WorkloadEventHandler4,
                Trading5WorkloadEventHandler5>()
            .BuildServiceProvider();
        eventBus = serviceProvider.GetRequiredService<IEventBus>();
        dynamic1Component = serviceProvider.GetRequiredService<Dynamic1Component>();
        dynamic5Component1 = serviceProvider.GetRequiredService<Dynamic5Component1>();
        dynamic5Component2 = serviceProvider.GetRequiredService<Dynamic5Component2>();
        dynamic5Component3 = serviceProvider.GetRequiredService<Dynamic5Component3>();
        dynamic5Component4 = serviceProvider.GetRequiredService<Dynamic5Component4>();
        dynamic5Component5 = serviceProvider.GetRequiredService<Dynamic5Component5>();
        dynamic5WorkloadComponent1 = serviceProvider.GetRequiredService<Dynamic5WorkloadComponent1>();
        dynamic5WorkloadComponent2 = serviceProvider.GetRequiredService<Dynamic5WorkloadComponent2>();
        dynamic5WorkloadComponent3 = serviceProvider.GetRequiredService<Dynamic5WorkloadComponent3>();
        dynamic5WorkloadComponent4 = serviceProvider.GetRequiredService<Dynamic5WorkloadComponent4>();
        dynamic5WorkloadComponent5 = serviceProvider.GetRequiredService<Dynamic5WorkloadComponent5>();
    }

    record Dynamic1Event : EventBase { }
    record Trading1Event : EventBase { }

    class Dynamic1Component
    {
        public Dynamic1Component(IDynamicEventCoordinator dynamicEventCoordinator) => dynamicEventCoordinator.Subscribe<Dynamic1Event>(OnUIEvent);
        void OnUIEvent(object? sender, Dynamic1Event @event) { }
    }
    class Trading1EventHandler : IAsyncEventHandler<Trading1Event> { public Task Handle(Trading1Event @event) => Task.CompletedTask; }

    record Dynamic5Event : EventBase { }
    record Trading5Event : EventBase { }

    class Dynamic5ComponentBase
    {
        public Dynamic5ComponentBase(IDynamicEventCoordinator dynamicEventCoordinator) => dynamicEventCoordinator.Subscribe<Dynamic5Event>(OnUIEvent);
        void OnUIEvent(object? sender, Dynamic5Event @event) { }
    }
    class Dynamic5Component1(IDynamicEventCoordinator dynamicEventCoordinator) : Dynamic5ComponentBase(dynamicEventCoordinator) { }
    class Dynamic5Component2(IDynamicEventCoordinator dynamicEventCoordinator) : Dynamic5ComponentBase(dynamicEventCoordinator) { }
    class Dynamic5Component3(IDynamicEventCoordinator dynamicEventCoordinator) : Dynamic5ComponentBase(dynamicEventCoordinator) { }
    class Dynamic5Component4(IDynamicEventCoordinator dynamicEventCoordinator) : Dynamic5ComponentBase(dynamicEventCoordinator) { }
    class Dynamic5Component5(IDynamicEventCoordinator dynamicEventCoordinator) : Dynamic5ComponentBase(dynamicEventCoordinator) { }

    class Trading5EventHandlerBase : IAsyncEventHandler<Trading5Event> { public Task Handle(Trading5Event @event) => Task.CompletedTask; }
    class Trading5EventHandler1 : Trading5EventHandlerBase { }
    class Trading5EventHandler2 : Trading5EventHandlerBase { }
    class Trading5EventHandler3 : Trading5EventHandlerBase { }
    class Trading5EventHandler4 : Trading5EventHandlerBase { }
    class Trading5EventHandler5 : Trading5EventHandlerBase { }

    record Dynamic5WorkloadEvent : EventBase { }
    record Trading5WorkloadEvent : EventBase { }

    class Dynamic5WorkloadComponentBase
    {
        public Dynamic5WorkloadComponentBase(IDynamicEventCoordinator dynamicEventCoordinator) => dynamicEventCoordinator.Subscribe<Dynamic5WorkloadEvent>(OnUIEvent);
        void OnUIEvent(object? sender, Dynamic5WorkloadEvent @event) => Thread.Sleep(1);
    }
    class Dynamic5WorkloadComponent1(IDynamicEventCoordinator dynamicEventCoordinator) : Dynamic5WorkloadComponentBase(dynamicEventCoordinator) { }
    class Dynamic5WorkloadComponent2(IDynamicEventCoordinator dynamicEventCoordinator) : Dynamic5WorkloadComponentBase(dynamicEventCoordinator) { }
    class Dynamic5WorkloadComponent3(IDynamicEventCoordinator dynamicEventCoordinator) : Dynamic5WorkloadComponentBase(dynamicEventCoordinator) { }
    class Dynamic5WorkloadComponent4(IDynamicEventCoordinator dynamicEventCoordinator) : Dynamic5WorkloadComponentBase(dynamicEventCoordinator) { }
    class Dynamic5WorkloadComponent5(IDynamicEventCoordinator dynamicEventCoordinator) : Dynamic5WorkloadComponentBase(dynamicEventCoordinator) { }

    class Trading5WorkloadEventHandlerBase : IAsyncEventHandler<Trading5WorkloadEvent> { public Task Handle(Trading5WorkloadEvent @event) => Task.Delay(1); }
    class Trading5WorkloadEventHandler1 : Trading5WorkloadEventHandlerBase { }
    class Trading5WorkloadEventHandler2 : Trading5WorkloadEventHandlerBase { }
    class Trading5WorkloadEventHandler3 : Trading5WorkloadEventHandlerBase { }
    class Trading5WorkloadEventHandler4 : Trading5WorkloadEventHandlerBase { }
    class Trading5WorkloadEventHandler5 : Trading5WorkloadEventHandlerBase { }

    [Benchmark]
    public void RaiseDynamic1Event()
    {
        eventBus.Raise(new Dynamic1Event());
    }

    [Benchmark]
    public async Task RaiseTrading1Event()
    {
        await eventBus.Raise(new Trading1Event());
    }

    [Benchmark]
    public void RaiseDynamic5Event()
    {
        eventBus.Raise(new Dynamic5Event());
    }

    [Benchmark]
    public async Task RaiseTrading5Event()
    {
        await eventBus.Raise(new Trading5Event());
    }

    [Benchmark]
    public void RaiseDynamic5WorkloadEvent()
    {
        eventBus.Raise(new Dynamic5WorkloadEvent());
    }

    [Benchmark]
    public async Task RaiseTrading5WorkloadEvent()
    {
        await eventBus.Raise(new Trading5WorkloadEvent());
    }
}
