using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.ReadModels
{
    public class DisabledItemsListView
    {
        public DisabledItemsListView()
            : this(Bootstrapper.ReadModelRepository)
        {   
        }

        internal DisabledItemsListView(IReadModelReadOnlyRepository readOnlyModelRepository)
        {
            this.Items = readOnlyModelRepository.Get<DisabledItemView>();
        }

        public IEnumerable<DisabledItemView> Items { get; }

        public DisabledItemView GetItem(Guid id)
        {
            return this.Items.FirstOrDefault(item => item.Id.Value == id);
        }
    }
}