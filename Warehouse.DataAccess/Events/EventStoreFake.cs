using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.Events.Base;

namespace Warehouse.DataAccess.Events
{
    public class EventStoreFake : IEventStore
    {
        private readonly List<Event> events = new List<Event>(); 

        public void Save(Event @event)
        {
            this.events.Add(@event);
        }

        public IEnumerable<Event> GetEventsById(Guid id)
        {
            return this.events.Where(evt => evt.Id.Equals(id)).ToArray();
        }
    }
}
