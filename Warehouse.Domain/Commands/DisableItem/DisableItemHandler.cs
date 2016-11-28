using System.Linq;
using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Commands.DisableItem
{
    public class DisableItemHandler : ICommandHandler<DisableItemCommand>
    {
        private readonly IEventStore eventStore;

        public DisableItemHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public void Handle(DisableItemCommand command)
        {
            var item = new Item(this.eventStore.GetEventsById(command.ItemId.Value));
            item.Disable();
            item.UncommitedEvents.ToList().ForEach(this.eventStore.Save);
        }
    }
}
