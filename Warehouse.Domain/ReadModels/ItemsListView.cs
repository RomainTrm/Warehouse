using System.Collections.Generic;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.ReadModels
{
    public class ItemsListView
    {
        private readonly IItemsListRepository itemsListRepository;

        public ItemsListView(IItemsListRepository itemsListRepository)
        {
            this.itemsListRepository = itemsListRepository;
        }

        public IEnumerable<ItemView> Items => this.itemsListRepository.GetItems();
    }
}
