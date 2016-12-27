using System;

namespace Warehouse.Domain.Events.Base
{
    public abstract class Event
    {
        protected Event(Guid id)
        {
            this.Id = id;
            this.Horodate = DateTime.Now;
        }

        public Guid Id { get; }

        public DateTime Horodate { get; }
    }
}
