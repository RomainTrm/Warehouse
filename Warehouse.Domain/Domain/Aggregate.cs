using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Domain
{
    public abstract class Aggregate
    {
        protected Aggregate(IEnumerable<IEvent> events)
        {
            var aggregatedEvents = this.GetAggregatedEvents();
            this.ApplyEvents(events, aggregatedEvents);
        }

        protected abstract IReadOnlyDictionary<Type, Action<IEvent>> GetAggregatedEvents();

        private void ApplyEvents(IEnumerable<IEvent> itemEvents, IReadOnlyDictionary<Type, Action<IEvent>> aggregatedEvents)
        {
            foreach (var itemEvent in itemEvents.Where(evt => aggregatedEvents.ContainsKey(evt.GetType())))
            {
                aggregatedEvents[itemEvent.GetType()].Invoke(itemEvent);
            }
        }

        protected readonly List<IEvent> UncommitedEventsList = new List<IEvent>();

        public IEnumerable<IEvent> UncommitedEvents
        {
            get { return this.UncommitedEventsList; }
        }
    }
}