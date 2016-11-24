using System;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events
{
    public class ItemRenamed : IEvent
    {
        public ItemRenamed(Guid itemId, string newName)
        {
            this.Id = itemId;
            this.NewName = newName;
        }

        public Guid Id { get; }

        public string NewName { get; }
    }
}