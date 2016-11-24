using System.Collections.Generic;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.ReadModels
{
    public class ItemsListView
    {
        public ItemsListView(IItemsListRepository itemsListRepository)
        {
            this.Items = itemsListRepository.GetItems();
        }

        public IEnumerable<ItemView> Items { get; }
    }
}
