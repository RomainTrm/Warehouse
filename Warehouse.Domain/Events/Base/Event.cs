using System;

namespace Warehouse.Domain.Events.Base
{
    public abstract class Event
    {
        public Guid Id { get; protected set; }
    }
}
