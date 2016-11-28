using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.RemoveUnits
{
    public class RemoveUnitsCommand : ICommand
    {
        public RemoveUnitsCommand(ItemId itemId, uint quantity)
        {
            this.ItemId = itemId;
            this.Quantity = quantity;
        }

        public ItemId ItemId { get; }

        public uint Quantity { get; }
    }
}
