using System.Linq;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.ReadModels.Repositories
{
    public class ViewModelGenerator : IEventHandler<ItemCreated>, IEventHandler<ItemRenamed>, IEventHandler<UnitsAdded>, IEventHandler<UnitsRemoved>, IEventHandler<ItemDisabled>, IEventHandler<ItemEnabled>
    {
        private readonly IReadModelRepository readModelRepository;

        public ViewModelGenerator(IReadModelRepository readModelRepository)
        {
            this.readModelRepository = readModelRepository;
        }

        public void Handle(ItemCreated @event)
        {
            var newItem = new ItemView(@event.Id, @event.Name);
            this.readModelRepository.Insert(newItem);
        }

        public void Handle(ItemRenamed @event)
        {
            var itemView = this.readModelRepository.Get<ItemView>().Single(x => x.Id.Value == @event.Id);
            itemView.Name = @event.NewName;
            this.readModelRepository.Update(itemView);
        }

        public void Handle(ItemDisabled @event)
        {
            var itemView = this.readModelRepository.Get<ItemView>().Single(x => x.Id.Value == @event.Id);
            this.readModelRepository.Delete(itemView);
            this.readModelRepository.Insert(new DisableItemView(itemView.Id.Value, itemView.Name));
	    }

        public void Handle(ItemEnabled @event)
        {
            var disableItemView = this.readModelRepository.Get<DisableItemView>().Single(x => x.Id.Value == @event.Id);
            this.readModelRepository.Delete(disableItemView);
            this.readModelRepository.Insert(new ItemView(disableItemView.Id.Value, disableItemView.Name));
        }

        public void Handle(UnitsAdded @event)
        {
            var itemView = this.readModelRepository.Get<ItemView>().Single(x => x.Id.Value == @event.Id);
            itemView.Units += @event.Units;
            this.readModelRepository.Update(itemView);
        }

        public void Handle(UnitsRemoved @event)
        {
            var itemView = this.readModelRepository.Get<ItemView>().Single(x => x.Id.Value == @event.Id);
            itemView.Units -= @event.Units;
            this.readModelRepository.Update(itemView);
        }
    }
}