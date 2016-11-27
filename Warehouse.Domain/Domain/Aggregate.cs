using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Domain
{
    public abstract class Aggregate
    {
        protected Aggregate(IEnumerable<Event> events)
        {
            var aggregatedEvents = this.GetAggregatedEvents();
            this.ApplyEvents(events, aggregatedEvents);
        }

        protected abstract IReadOnlyDictionary<Type, Action<Event>> GetAggregatedEvents();

        private void ApplyEvents(IEnumerable<Event> itemEvents, IReadOnlyDictionary<Type, Action<Event>> aggregatedEvents)
        {
            foreach (var itemEvent in itemEvents.Where(evt => aggregatedEvents.ContainsKey(evt.GetType())))
            {
                aggregatedEvents[itemEvent.GetType()].Invoke(itemEvent);
            }
        }

        protected readonly List<Event> UncommitedEventsList = new List<Event>();

        public IEnumerable<Event> UncommitedEvents
        {
            get { return this.UncommitedEventsList; }
        }
    }
}