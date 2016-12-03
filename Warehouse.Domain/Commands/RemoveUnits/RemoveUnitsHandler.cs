using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Commands.RemoveUnits
{
    internal class RemoveUnitsHandler : ICommandHandler<RemoveUnitsCommand>
    {
        private readonly IEventStore eventStore;

        public RemoveUnitsHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public void Handle(RemoveUnitsCommand command)
        {
            var item = new Item(this.eventStore.GetEventsById(command.ItemId.Value));
            item.RemoveUnits(command.Quantity);
            this.eventStore.Save(item.UncommitedEvents);
        }
    }
}