using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.CreateItem
{
    public class CreateItemCommand : ICommand
    {
        public CreateItemCommand(string itemName)
        {
            this.ItemName = itemName;
        }

        public string ItemName { get; }
    }
}