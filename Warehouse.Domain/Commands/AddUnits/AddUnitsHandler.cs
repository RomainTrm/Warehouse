using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Commands.AddUnits
{
    internal class AddUnitsHandler : ICommandHandler<AddUnitsCommand>
    {
        private readonly IEventStore eventStore;

        public AddUnitsHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public void Handle(AddUnitsCommand command)
        {
            var item = new Item(this.eventStore.GetEventsById(command.ItemId.Value));
            item.AddUnits(command.Quantity);
            this.eventStore.Save(item.UncommitedEvents);
        }
    }
}
