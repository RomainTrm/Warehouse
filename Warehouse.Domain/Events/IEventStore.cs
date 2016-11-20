namespace Warehouse.Domain.Events
{
    public interface IEventStore
    {
        void Save(Event @event);
    }
}
