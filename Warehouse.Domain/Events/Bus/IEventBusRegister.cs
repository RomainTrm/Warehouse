using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events.Bus
{
    public interface IEventBusRegister
    {
        void Register<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : IEvent;
    }
}