using System;
using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Commands.RenameItem
{
    public class RenameItemHandler : ICommandHandler<RenameItemCommand>
    {
        private readonly IEventStore eventStore;

        public RenameItemHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public void Handle(RenameItemCommand command)
        {
            var item = new Item(this.eventStore.GetEventsById(command.ItemItemId));
            item.Rename(command.NewName);

            foreach (var uncommitedEvent in item.UncommitedEvents)
            {
                this.eventStore.Save(uncommitedEvent);
            }
        }
    }
}