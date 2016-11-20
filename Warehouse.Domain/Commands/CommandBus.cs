using System.Collections.Generic;
using System.Linq;

namespace Warehouse.Domain.Commands
{
    public class CommandBus : ICommandBus, ICommandBusRegister
    {
        private readonly List<ICommandHandler> handlers = new List<ICommandHandler>();

        public void RegsiterHandler<TCommand>(ICommandHandler<TCommand> commandHandler)
            where TCommand : ICommand
        {
            if (commandHandler == null)
            {
                throw new CommandBusException("Vous ne pouvez pas enregister un handler null.");
            }

            var expectedHandlers = this.handlers.OfType<ICommandHandler<TCommand>>().ToArray();
            if (expectedHandlers.Any())
            {
                throw new CommandBusException($"Plus de un handler pour ce type de command {typeof(TCommand)}.");
            }

            this.handlers.Add(commandHandler);
        }

        public void Send<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var expectedHandlers = this.handlers.OfType<ICommandHandler<TCommand>>().ToArray();

            if (!expectedHandlers.Any())
            {
                throw new CommandBusException($"Aucun handler pour ce type de command {typeof(TCommand)}.");
            }

            expectedHandlers.Single().Handle(command);
        }
    }
}