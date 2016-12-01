using System.Collections.Generic;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.ReadModels
{
    public class DisabledItemsListView
    {
        public DisabledItemsListView(IReadModelReadOnlyRepository readOnlyModelRepository)
        {
            this.Items = readOnlyModelRepository.Get<DisabledItemView>();
        }

        public IEnumerable<DisabledItemView> Items { get; }
    }
}