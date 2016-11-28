using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.ReadModels.Base;

namespace Warehouse.Domain.ReadModels.Repositories
{
    public class ItemsListRepository : IItemsListRepository, IEventHandler<ItemCreated>, IEventHandler<ItemRenamed>, IEventHandler<UnitsAdded>, IEventHandler<UnitsRemoved>, IEventHandler<ItemDisabled>
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

        public IEnumerable<DisableItemView> GetDisableItems()
        {
            return this.repository.Get<DisableItemView>();
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

        public void Handle(ItemDisabled @event)
        {
            var itemView = this.repository.Get<ItemView>().Single(x => x.Id.Value == @event.Id);
            this.repository.Delete(itemView);
            this.repository.Insert(new DisableItemView(itemView.Id.Value, itemView.Name));
	    }

        public void Handle(UnitsAdded @event)
        {
            var itemView = this.repository.Get<ItemView>().Single(x => x.Id.Value == @event.Id);
            itemView.Units += @event.Units;
            this.repository.Update(itemView);
        }

        public void Handle(UnitsRemoved @event)
        {
            var itemView = this.repository.Get<ItemView>().Single(x => x.Id.Value == @event.Id);
            itemView.Units -= @event.Units;
            this.repository.Update(itemView);
        }
    }
}