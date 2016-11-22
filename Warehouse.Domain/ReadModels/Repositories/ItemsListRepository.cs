using System.Collections.Generic;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.ReadModels.Base;

namespace Warehouse.Domain.ReadModels.Repositories
{
    public class ItemsListRepository : IItemsListRepository, IEventHandler<ItemCreated>
    {
        private readonly IRepository repository;

        public ItemsListRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<ItemView> GetItems()
        {
            return this.repository.Get<ItemView>();
        }

        public void Handle(ItemCreated @event)
        {
            var newItem = new ItemView(@event.Id, @event.Name);
            this.repository.Insert(newItem);
        }
    }
}