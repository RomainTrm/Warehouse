using System;
using Warehouse.Domain.Events.Base;

namespace Warehouse.DataAccess.Events
{
    public class EventContainer
    {
        public EventContainer(Event @event, DateTime dateTime)
        {
            this.DomainEvent = @event;
            this.DateTime = dateTime;
        }

        public Event DomainEvent { get; }

        public DateTime DateTime { get; }

        public override bool Equals(object obj)
        {
            return obj is EventContainer && this.Equals((EventContainer) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.DomainEvent != null ? this.DomainEvent.GetHashCode() : 0)*397) ^ this.DateTime.GetHashCode();
            }
        }

        private bool Equals(EventContainer other)
        {
            return Equals(this.DomainEvent, other.DomainEvent) && this.DateTime.Equals(other.DateTime);
        }
    }
}