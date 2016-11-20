namespace Warehouse.Domain.Commands
{
    public interface ICommandBusRegister
    {
        void RegsiterHandler<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : ICommand;
    }
}