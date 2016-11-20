namespace Warehouse.Domain.Commands
{
    public interface ICommandBus
    {
        void RegsiterHandler<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : ICommand;

        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}