using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.RenameItem
{
    public class RenameItemCommand : ICommand
    {
        public RenameItemCommand(ItemId itemId, string newName)
        {
            this.ItemId = itemId;
            this.NewName = newName;
        }

        public ItemId ItemId { get; }

        public string NewName { get; }
    }
}
