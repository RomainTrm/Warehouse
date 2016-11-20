namespace Warehouse.Domain.Events
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event)
            where TEvent : Event;
    }
}
