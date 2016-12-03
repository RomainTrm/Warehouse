namespace Warehouse.Domain.Commands.Base
{
    internal interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    internal interface ICommandHandler { }
}