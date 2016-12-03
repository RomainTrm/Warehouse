using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Commands.Exceptions;

namespace Warehouse.Domain.Commands.Bus
{
    internal class CommandBus : ICommandBus, ICommandBusRegister
    {
        private readonly List<ICommandHandler> handlers = new List<ICommandHandler>();

        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> commandHandler)
            where TCommand : ICommand
        {
            if (commandHandler == null)
            {
                throw new CommandBusException("You can't register a null handler.");
            }

            var expectedHandlers = this.handlers.OfType<ICommandHandler<TCommand>>().ToArray();
            if (expectedHandlers.Any())
            {
                throw new CommandBusException($"There's already one handler registered for command type {nameof(TCommand)}.");
            }

            this.handlers.Add(commandHandler);
        }

        public void Send<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var expectedHandlers = this.handlers.OfType<ICommandHandler<TCommand>>().ToArray();

            if (!expectedHandlers.Any())
            {
                throw new CommandBusException($"There's no handler registered for command type {nameof(TCommand)}.");
            }

            expectedHandlers.Single().Handle(command);
        }
    }
}