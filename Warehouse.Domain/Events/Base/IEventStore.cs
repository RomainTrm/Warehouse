using System;
using System.Collections.Generic;

namespace Warehouse.Domain.Events.Base
{
    public interface IEventStore
    {
        void Save<TEvent>(TEvent @event) where TEvent : IEvent;

        IEnumerable<IEvent> GetEventsById(Guid id);
    }
}
