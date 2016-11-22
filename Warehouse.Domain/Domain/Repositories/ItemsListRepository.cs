using System.Collections.Generic;
using Warehouse.Domain.Domain.Base;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Domain.Repositories
{
    public class ItemsListRepository : IItemsListRepository, IEventHandler<ItemCreated>
    {
        private readonly IRepository repository;

        public ItemsListRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Item> GetItems()
        {
            return this.repository.Get<Item>();
        }

        public void Handle(ItemCreated @event)
        {
            var newItem = new Item(@event.ItemId, @event.ItemName);
            this.repository.Insert(newItem);
        }
    }
}