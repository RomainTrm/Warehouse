using System;
using System.Collections.Generic;

namespace Warehouse.Domain.Events.Base
{
    public interface IEventStore
    {
        void Save(Event @event);

        void Save(IEnumerable<Event> events);

        IEnumerable<Event> GetEventsById(Guid id);
    }
}
