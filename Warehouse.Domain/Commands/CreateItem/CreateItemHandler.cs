using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Commands.Exceptions;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Commands.CreateItem
{
    internal class CreateItemHandler : ICommandHandler<CreateItemCommand>
    {
        private readonly IEventStore eventStore;

        public CreateItemHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public void Handle(CreateItemCommand command)
        {
            if (string.IsNullOrEmpty(command.ItemName))
            {
                throw new CommandHandlerException("You can't create an Item with an empty name.");
            }

            this.eventStore.Save(new ItemCreated(command.ItemName));
        }
    }
}
