using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.Events.Bus;

namespace Warehouse.DataAccess.Events
{
    public class EventStoreFake : IEventStore
    {
        private readonly IEventBus eventBus;
        private readonly List<Event> events = new List<Event>();

        public EventStoreFake(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public void Save(Event @event)
        {
            this.events.Add(@event);
            this.eventBus.Publish(@event);
        }

        public IEnumerable<Event> GetEventsById(Guid id)
        {
            return this.events.Where(evt => evt.Id.Equals(id)).ToArray();
        }
    }
}
