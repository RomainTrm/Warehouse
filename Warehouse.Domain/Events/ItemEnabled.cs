using System;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events
{
    public class ItemEnabled : Event
    {
        public ItemEnabled(Guid itemId)
            : base(itemId)
        {
        }
    }
}