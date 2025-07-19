using EventBusNet.EventHandlers;
using EventBusNet.Extensions;
using EventBusNet.PipelineMiddlewares;
using EventBusNet.Raisers;
using EventBusNet.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusNet.Demo;

internal class Program
{
    record DynamicEvent : EventBase { }
    record BusinessEvent1 : EventBase { }
    record BusinessEvent2 : EventBase { }
    record BusinessEvent3 : EventBase { }

    interface IDynamicComponent { }
    class DynamicComponent : IDynamicComponent
    {
        private readonly IDynamicEventCoordinator dynamicEventCoordinator = default!;
        public DynamicComponent(IDynamicEventCoordinator dynamicEventCoordinator)
        {
            this.dynamicEventCoordinator = dynamicEventCoordinator;
            this.dynamicEventCoordinator.Subscribe<DynamicEvent>(this.OnDynamicEvent);
            this.dynamicEventCoordinator.Subscribe<BusinessEvent1>(this.OnBusinessEvent1);
            this.dynamicEventCoordinator.Subscribe<BusinessEvent2>(this.OnBusinessEvent2);
            this.dynamicEventCoordinator.Subscribe<BusinessEvent3>(this.OnBusinessEvent3);
        }

        public void Close()
        {
            this.dynamicEventCoordinator.Unsubscribe<DynamicEvent>(this.OnDynamicEvent);
            this.dynamicEventCoordinator.Unsubscribe<BusinessEvent1>(this.OnBusinessEvent1);
            this.dynamicEventCoordinator.Unsubscribe<BusinessEvent2>(this.OnBusinessEvent2);
            this.dynamicEventCoordinator.Unsubscribe<BusinessEvent3>(this.OnBusinessEvent3);
        }

        private void OnDynamicEvent(object? sender, DynamicEvent e) { }
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

    class CustomPipelineMiddleware : IPipelineMiddleware { public void Process(EventBase @event) { } }
    class CustomBusiness1PipelineMiddleware : IEventPipelineMiddleware<BusinessEvent1> { public void Process(BusinessEvent1 @event) { } }
    class CustomDynamicPipelineMiddleware : IEventPipelineMiddleware<DynamicEvent> { public void Process(DynamicEvent @event) { } }

    static void Main(string[] args)
    {
        var services = new ServiceCollection()
            .AddEventBusNet(options =>
            {
                options.Logging = true;
            })
            .AddSingleton<IPipelineMiddleware, CustomPipelineMiddleware>()
            .AddSingleton<IEventPipelineMiddleware<DynamicEvent>, CustomDynamicPipelineMiddleware>()
            .AddSingleton<IEventPipelineMiddleware<BusinessEvent1>, CustomBusiness1PipelineMiddleware>()
            .AddEvent<BusinessEvent1, IBusinessComponent1>()
            .AddEvent<BusinessEvent2, IBusinessComponent2>()
            .AddEvent<BusinessEvent3, IBusinessComponent3>()
            .SubscribeEvent<IBusinessComponentAll, BusinessEvent1, BusinessEvent2, BusinessEvent3>()
            .AddSingleton<IDynamicComponent, DynamicComponent>()
            .AddSingleton<IBusinessComponent1, BusinessComponent1>()
            .AddSingleton<IBusinessComponent2, BusinessComponent2>()
            .AddSingleton<IBusinessComponent3, BusinessComponent3>()
            .AddSingleton<IBusinessComponentAll, BusinessComponentAll>();
        var serviceProvider = services.BuildServiceProvider();

        var dynamicComponent = serviceProvider.GetRequiredService<IDynamicComponent>();
        var eventRaiser = serviceProvider.GetRequiredService<IEventRaiser>();

        eventRaiser.Raise(new DynamicEvent());
        eventRaiser.Raise(new BusinessEvent1());
        eventRaiser.Raise(new BusinessEvent2());
        eventRaiser.Raise(new BusinessEvent3());
    }
}
