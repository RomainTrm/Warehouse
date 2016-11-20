namespace Warehouse.Domain.Events
{
    public abstract class Event
    {
        public int Version { get; protected set; }
    }
}
