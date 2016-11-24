using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.ReadModels.Base;

namespace Warehouse.Domain.ReadModels.Repositories
{
    public class ItemsListRepository : IItemsListRepository, IEventHandler<ItemCreated>, IEventHandler<ItemRenamed>
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

        public void Handle(ItemRenamed @event)
        {
            var itemView = this.repository.Get<ItemView>().Single(x => x.Id.Value == @event.Id);
            itemView.Name = @event.NewName;
            this.repository.Update(itemView);
        }
    }
}