using System;
using Warehouse.Domain.Commands.Base;
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
            throw new NotImplementedException();
        }
    }
}