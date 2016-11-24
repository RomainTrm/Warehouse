using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.RenameItem
{
    public class RenameItemCommand : ICommand
    {
        public RenameItemCommand(ItemId itemId, string newName)
        {
            this.ItemItemId = itemId;
            this.NewName = newName;
        }

        public ItemId ItemItemId { get; }

        public string NewName { get; }
    }
}
