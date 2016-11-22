using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Commands.Exceptions;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Commands
{
    public class CreateItemHandler : ICommandHandler<CreateItemCommand>
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
                throw new CommandHandlerException($"Le nom de l'item ne peut pas être vide pour la commande {nameof(CreateItemCommand)}");
            }

            this.eventStore.Save(new ItemCreated(command.ItemName));
        }
    }
}
