using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.Bus
{
    public interface ICommandBusRegister
    {
        void RegsiterHandler<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : ICommand;
    }
}