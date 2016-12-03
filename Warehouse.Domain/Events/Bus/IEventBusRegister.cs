using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events.Bus
{
    internal interface IEventBusRegister
    {
        void Register<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : Event;
    }
}