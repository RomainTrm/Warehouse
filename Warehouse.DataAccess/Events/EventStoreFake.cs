using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.Events.Bus;

namespace Warehouse.DataAccess.Events
{
    public class EventStoreFake : IEventStoreRegistration
    {
        private readonly List<Event> events = new List<Event>();

        private IEventBus eventBus;

        public void Save(Event @event)
        {
            this.events.Add(@event);
            this.eventBus.Publish(@event);
        }

        public void Save(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                this.Save(@event);
            }
        }

        public IEnumerable<Event> GetEventsById(Guid id)
        {
            return this.events.Where(evt => evt.Id.Equals(id)).ToArray();
        }

        public void SetEventBusToPublish(IEventBus eventBus)
        {
            if (this.eventBus != null)
            {
                throw new InvalidOperationException("EventBus is already initialized.");
            }

            this.eventBus = eventBus;
        }
    }
}
