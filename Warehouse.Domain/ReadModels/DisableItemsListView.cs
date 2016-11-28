using System.Collections.Generic;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.ReadModels
{
    public class DisableItemsListView
    {
        public DisableItemsListView(IItemsListRepository itemsListRepository)
        {
            this.Items = itemsListRepository.GetDisableItems();
        }

        public IEnumerable<DisableItemView> Items { get; }
    }
}