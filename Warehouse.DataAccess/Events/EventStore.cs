using Warehouse.Domain.Events.Base;

namespace Warehouse.DataAccess.Events
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreAccess eventStoreAccess;
        private readonly IDateTimeProvider dateTimeProvider;

        internal EventStore(IEventStoreAccess eventStoreAccess, IDateTimeProvider dateTimeProvider)
        {
            this.eventStoreAccess = eventStoreAccess;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Save(Event @event)
        {
            var container = new EventContainer(@event, this.dateTimeProvider.GetDateTime());
            this.eventStoreAccess.Push(container);
        }
    }
}
