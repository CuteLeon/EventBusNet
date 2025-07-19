# EventBusNet

EventBusNet is a high-performance, extensible, and easy-to-use event bus library for .NET, designed to decouple event publishing and handling in modern applications. It supports both synchronous and asynchronous event processing, dynamic event subscription, and middleware pipelines, making it suitable for a wide range of scenarios from UI to backend services.

---

## Benchmark Results

EventBusNet is engineered for speed. 
* [Benchmark Results](EventBusNet.Benchmark/BenchmarkArtifacts/results/EventBusNet.Benchmark.Benchmark_EventBusNet-report-github.md)

---

## Features

- **High Performance**: Ultra-low overhead for event dispatching, as demonstrated by comprehensive benchmarks.
- **Unified Event Model**: Supports both static (compile-time) and dynamic (runtime) event subscription.
- **Asynchronous & Synchronous Handling**: Native support for async event handlers and dynamic event coordination.
- **Middleware Pipeline**: Easily extend event processing with custom middleware (e.g., logging, validation).
- **Dependency Injection Ready**: Seamless integration with `Microsoft.Extensions.DependencyInjection`.
- **Strong Typing**: Leverages C# 12 and .NET 8 features for type safety and performance.
- **Extensible**: Easily add custom event handlers, middlewares, and configuration.

---

## Quick Start

```csharp
    using EventBusNet.EventHandlers;
    using EventBusNet.Extensions;
    using EventBusNet.Raisers;
    using EventBusNet.Utils;
    using Microsoft.Extensions.DependencyInjection;

    var services = new ServiceCollection()
        .AddEventBusNet(options =>
        {
            options.Logging = true;
        })
    #if DEBUG
        services.ValidateEventBusNet();
    #endif

    var serviceProvider = services.BuildServiceProvider();

    var eventRaiser = serviceProvider.GetRequiredService<IEventRaiser>();
    eventRaiser.Raise(new MyEvent());
```

### Dynamic Event Subscription
```csharp
    record DynamicEvent : EventBase { }

    interface IDynamicComponent { }
    class DynamicComponent : IDynamicComponent
    {
        private readonly IDynamicEventCoordinator dynamicEventCoordinator = default!;
        public DynamicComponent(IDynamicEventCoordinator dynamicEventCoordinator)
        {
            this.dynamicEventCoordinator = dynamicEventCoordinator;
            this.dynamicEventCoordinator.Subscribe<DynamicEvent>(this.OnDynamicEvent);
        }

        public void Close()
        {
            this.dynamicEventCoordinator.Unsubscribe<DynamicEvent>(this.OnDynamicEvent);
        }

        private void OnDynamicEvent(object? sender, DynamicEvent e) { }
    }

    services
        .AddSingleton<IDynamicComponent, DynamicComponent>();

    var serviceProvider = services.BuildServiceProvider();

    var dynamicComponent = serviceProvider.GetRequiredService<IDynamicComponent>();
    var eventRaiser = serviceProvider.GetRequiredService<IEventRaiser>();

    eventRaiser.Raise(new DynamicEvent());
```

### Async Event Handling
```csharp
    record BusinessEvent1 : EventBase { }
    record BusinessEvent2 : EventBase { }
    record BusinessEvent3 : EventBase { }

    class DynamicComponent : IDynamicComponent
    {
        private readonly IDynamicEventCoordinator dynamicEventCoordinator = default!;
        public DynamicComponent(IDynamicEventCoordinator dynamicEventCoordinator)
        {
            this.dynamicEventCoordinator = dynamicEventCoordinator;
            this.dynamicEventCoordinator.Subscribe<BusinessEvent1>(this.OnBusinessEvent1);
            this.dynamicEventCoordinator.Subscribe<BusinessEvent2>(this.OnBusinessEvent2);
            this.dynamicEventCoordinator.Subscribe<BusinessEvent3>(this.OnBusinessEvent3);
        }

        public void Close()
        {
            this.dynamicEventCoordinator.Unsubscribe<BusinessEvent1>(this.OnBusinessEvent1);
            this.dynamicEventCoordinator.Unsubscribe<BusinessEvent2>(this.OnBusinessEvent2);
            this.dynamicEventCoordinator.Unsubscribe<BusinessEvent3>(this.OnBusinessEvent3);
        }

        private void OnBusinessEvent1(object? sender, BusinessEvent1 e) { }
        private void OnBusinessEvent2(object? sender, BusinessEvent2 e) { }
        private void OnBusinessEvent3(object? sender, BusinessEvent3 e) { }
    }

    interface IBusinessComponent1 : IAsyncEventHandler<BusinessEvent1> { }
    interface IBusinessComponent2 : IAsyncEventHandler<BusinessEvent2> { }
    interface IBusinessComponent3 : IAsyncEventHandler<BusinessEvent3> { }
    interface IBusinessComponentAll : IAsyncEventHandler<BusinessEvent1>, IAsyncEventHandler<BusinessEvent2>, IAsyncEventHandler<BusinessEvent3> { }
    class BusinessComponent1 : IBusinessComponent1 { public Task Handle(BusinessEvent1 @event) => Task.CompletedTask; }
    class BusinessComponent2 : IBusinessComponent2 { public Task Handle(BusinessEvent2 @event) => Task.CompletedTask; }
    class BusinessComponent3 : IBusinessComponent3 { public Task Handle(BusinessEvent3 @event) => Task.CompletedTask; }
    class BusinessComponentAll : IBusinessComponentAll
    {
        public Task Handle(BusinessEvent1 @event) => Task.CompletedTask;
        public Task Handle(BusinessEvent2 @event) => Task.CompletedTask;
        public Task Handle(BusinessEvent3 @event) => Task.CompletedTask;
    }

    services
        .AddEvent<BusinessEvent1, IBusinessComponent1>()
        .AddEvent<BusinessEvent2, IBusinessComponent2>()
        .AddEvent<BusinessEvent3, IBusinessComponent3>()
        .SubscribeEvent<IBusinessComponentAll, BusinessEvent1, BusinessEvent2, BusinessEvent3>()
        .AddSingleton<IBusinessComponent1, BusinessComponent1>()
        .AddSingleton<IBusinessComponent2, BusinessComponent2>()
        .AddSingleton<IBusinessComponent3, BusinessComponent3>()
        .AddSingleton<IBusinessComponentAll, BusinessComponentAll>();

    var serviceProvider = services.BuildServiceProvider();

    var eventRaiser = serviceProvider.GetRequiredService<IEventRaiser>();

    eventRaiser.Raise(new BusinessEvent1());
    eventRaiser.Raise(new BusinessEvent2());
    eventRaiser.Raise(new BusinessEvent3());
```

### Pipelines and Middleware
```csharp
    class CustomPipelineMiddleware : IPipelineMiddleware { public void Process(EventBase @event) { } }
    class CustomBusiness1PipelineMiddleware : IEventPipelineMiddleware<BusinessEvent1> { public void Process(BusinessEvent1 @event) { } }
    class CustomDynamicPipelineMiddleware : IEventPipelineMiddleware<DynamicEvent> { public void Process(DynamicEvent @event) { } }

    services
        .AddSingleton<IPipelineMiddleware, CustomPipelineMiddleware>()
        .AddSingleton<IEventPipelineMiddleware<DynamicEvent>, CustomDynamicPipelineMiddleware>()
        .AddSingleton<IEventPipelineMiddleware<BusinessEvent1>, CustomBusiness1PipelineMiddleware>();

    var serviceProvider = services.BuildServiceProvider();

    var dynamicComponent = serviceProvider.GetRequiredService<IDynamicComponent>();
    var eventRaiser = serviceProvider.GetRequiredService<IEventRaiser>();

    eventRaiser.Raise(new DynamicEvent());
    eventRaiser.Raise(new BusinessEvent1());
```

---

## Advanced Usage

- **Middleware**: Add custom logic (logging, validation, etc.) to the event pipeline.
- **Validation**: Use `services.ValidateEventBusNet()` to ensure correct handler registration.
- **Dynamic Event Aggregation**: Subscribe/unsubscribe handlers at runtime for UI or plugin scenarios.

---

## Why EventBusNet?

- **Performance**: Outperforms most existing .NET event bus libraries in microbenchmarks.
- **Flexibility**: Supports both static and dynamic event models.
- **Modern Design**: Built with the latest .NET and C# features.
- **Production Ready**: Designed for reliability and extensibility in real-world applications.

---