using System;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Events
{
    public class ItemDisabled : Event
    {
        public ItemDisabled(Guid itemId)
            : base(itemId)
        {
        }
    }
}
