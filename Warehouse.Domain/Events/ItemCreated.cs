using System;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events
{
    public class ItemCreated : Event
    {
        public ItemCreated(string itemName)
        {
            this.ItemId = Guid.NewGuid();
            this.ItemName = itemName;
        }

        public Guid ItemId { get; }

        public string ItemName { get; }
    }
}
