using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.EnableItem
{
    public class EnableItemCommand : ICommand
    {
        public EnableItemCommand(ItemId itemId)
        {
            this.ItemId = itemId;
        }

        public ItemId ItemId { get; }
    }
}
