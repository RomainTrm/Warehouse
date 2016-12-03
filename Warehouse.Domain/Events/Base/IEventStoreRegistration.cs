using Warehouse.Domain.Events.Bus;

namespace Warehouse.Domain.Events.Base
{
    public interface IEventStoreRegistration : IEventStore
    {
        void SetEventBusToPublish(IEventBus eventBus);
    }
}