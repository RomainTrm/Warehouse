using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.ReadModels
{
    public class ItemsListView
    {
        public ItemsListView()
            : this(Bootstrapper.ReadModelRepository)
        { 
        }

        internal ItemsListView(IReadModelReadOnlyRepository readOnlyModelRepository)
        {
            this.Items = readOnlyModelRepository.Get<ItemView>();
        }

        public IEnumerable<ItemView> Items { get; }

        public ItemView GetItem(Guid id)
        {
            return this.Items.SingleOrDefault(item => item.Id.Value == id);
        }
    }
}
