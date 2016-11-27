using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events.Bus
{
    public interface IEventBus
    {
        void Publish(Event @event);
    }
}
