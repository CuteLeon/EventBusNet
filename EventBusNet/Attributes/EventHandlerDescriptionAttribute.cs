namespace EventBusNet.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
public class EventHandlerDescriptionAttribute : Attribute
{
    public int PriorityIndex { get; set; } = 0;
}
