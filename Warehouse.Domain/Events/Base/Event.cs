using System;

namespace Warehouse.Domain.Events.Base
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}
