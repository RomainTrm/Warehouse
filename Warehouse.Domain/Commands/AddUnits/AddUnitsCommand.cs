using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.AddUnits
{
    public class AddUnitsCommand : ICommand
    {
        public AddUnitsCommand(ItemId itemId, uint quantity)
        {
            this.ItemId = itemId;
            this.Quantity = quantity;
        }

        public ItemId ItemId { get; }

        public uint Quantity { get; }
    }
}
