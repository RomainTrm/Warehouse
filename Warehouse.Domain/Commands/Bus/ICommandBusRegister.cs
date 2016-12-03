using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.Bus
{
    internal interface ICommandBusRegister
    {
        void RegisterHandler<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : ICommand;
    }
}