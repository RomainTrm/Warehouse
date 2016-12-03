using System;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events
{
    internal class ItemCreated : Event
    {
        public ItemCreated(string name)
            : base(Guid.NewGuid())
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}
