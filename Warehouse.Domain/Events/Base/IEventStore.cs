namespace Warehouse.Domain.Events.Base
{
    public interface IEventStore
    {
        void Save(Event @event);
    }
}
