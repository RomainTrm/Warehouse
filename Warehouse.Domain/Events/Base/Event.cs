namespace Warehouse.Domain.Events.Base
{
    public abstract class Event
    {
        public int Version { get; protected set; }
    }
}
