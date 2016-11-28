using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Commands.EnableItem
{
    public class EnableItemHandler : ICommandHandler<EnableItemCommand>
    {
        private readonly IEventStore eventStore;

        public EnableItemHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public void Handle(EnableItemCommand command)
        {
            var item = new Item(this.eventStore.GetEventsById(command.ItemId.Value));
            item.Enable();
            this.eventStore.Save(item.UncommitedEvents);
        }
    }
}