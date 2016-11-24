using System;
using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.DisableItem
{
    public class DisableItemCommand : ICommand
    {
        public DisableItemCommand(Guid itemId)
        {
            this.ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}
