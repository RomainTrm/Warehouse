using System;

namespace Warehouse.Domain.Events.Base
{
    public abstract class Event
    {
        protected Event(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
