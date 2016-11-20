namespace Warehouse.Domain.Events
{
    public interface IEventBusRegister
    {
        void Register<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : Event;
    }
}