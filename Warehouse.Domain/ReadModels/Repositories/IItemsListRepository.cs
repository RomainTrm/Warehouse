using System.Collections.Generic;

namespace Warehouse.Domain.ReadModels.Repositories
{
    public interface IItemsListRepository
    {
        IEnumerable<ItemView> GetItems();

        IEnumerable<DisableItemView> GetDisableItems();
    }
}
