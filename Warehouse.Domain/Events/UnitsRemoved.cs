using System;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events
{
    internal class UnitsRemoved : Event
    {
        public UnitsRemoved(Guid id, uint units)
            : base(id)
        {
            this.Units = units;
        }

        public uint Units { get; }
    }
}