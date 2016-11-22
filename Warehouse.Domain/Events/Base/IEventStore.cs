using System.Collections.Generic;

namespace Warehouse.Domain.Events.Base
{
    public interface IEventStore
    {
        void Save(Event @event);

        IEnumerable<Event> GetEvents();
    }
}
