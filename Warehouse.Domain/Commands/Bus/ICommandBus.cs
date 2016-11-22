using Warehouse.Domain.Commands.Base;

namespace Warehouse.Domain.Commands.Bus
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}