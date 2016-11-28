using System;
using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.DisableItem
{
    public class DisableItemCommand : ICommand
    {
        public DisableItemCommand(ItemId itemId)
        {
            this.ItemId = itemId;
        }

        public ItemId ItemId { get; }
    }
}
