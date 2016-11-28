using System.Collections.Generic;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.ReadModels
{
    public class DisableItemsListView
    {
        public DisableItemsListView(IReadModelRepository readModelRepository)
        {
            this.Items = readModelRepository.Get<DisableItemView>();
        }

        public IEnumerable<DisableItemView> Items { get; }
    }
}