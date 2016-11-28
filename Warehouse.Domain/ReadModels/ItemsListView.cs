using System.Collections.Generic;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.ReadModels
{
    public class ItemsListView
    {
        public ItemsListView(IReadModelRepository readModelRepository)
        {
            this.Items = readModelRepository.Get<ItemView>();
        }

        public IEnumerable<ItemView> Items { get; }
    }
}
