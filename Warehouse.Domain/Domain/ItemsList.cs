using System.Collections.Generic;
using Warehouse.Domain.Domain.Repositories;

namespace Warehouse.Domain.Domain
{
    public class ItemsList
    {
        private readonly IItemsListRepository itemsListRepository;

        public ItemsList(IItemsListRepository itemsListRepository)
        {
            this.itemsListRepository = itemsListRepository;
        }

        public IEnumerable<Item> Items => this.itemsListRepository.GetItems();
    }
}
