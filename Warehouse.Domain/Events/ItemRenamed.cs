using System;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events
{
    public class ItemRenamed : Event
    {
        public ItemRenamed(Guid itemId, string newName)
            : base(itemId)
        {
            this.NewName = newName;
        }

        public string NewName { get; }
    }
}