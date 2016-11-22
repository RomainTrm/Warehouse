using System;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events
{
    public class ItemRenamed : Event
    {
        public ItemRenamed(Guid itemId, string newName)
        {
            this.Id = itemId;
            this.NewName = newName;
        }

        public string NewName { get; }
    }
}