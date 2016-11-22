using System;
using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.RenameItem
{
    public class RenameItemCommand : ICommand
    {
        public RenameItemCommand(Guid itemId, string newName)
        {
            this.ItemItemId = itemId;
            this.NewName = newName;
        }

        public Guid ItemItemId { get; }

        public string NewName { get; }
    }
}
