using System;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events
{
    internal class ItemEnabled : Event
    {
        public ItemEnabled(Guid itemId)
            : base(itemId)
        {
        }
    }
}