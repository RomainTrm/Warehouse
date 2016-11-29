using System.Collections.Generic;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.ReadModels
{
    public class DisableItemsListView
    {
        public DisableItemsListView(IReadModelReadOnlyRepository readOnlyModelRepository)
        {
            this.Items = readOnlyModelRepository.Get<DisableItemView>();
        }

        public IEnumerable<DisableItemView> Items { get; }
    }
}