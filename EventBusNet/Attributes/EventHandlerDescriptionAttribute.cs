namespace EventBusNet.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class EventHandlerDescriptionAttribute : Attribute
{
    public int PriorityIndex { get; set; } = 0;
}
