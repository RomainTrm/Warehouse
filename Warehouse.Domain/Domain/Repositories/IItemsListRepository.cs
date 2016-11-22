using System.Collections.Generic;

namespace Warehouse.Domain.Domain.Repositories
{
    public interface IItemsListRepository
    {
        IEnumerable<Item> GetItems();
    }
}
