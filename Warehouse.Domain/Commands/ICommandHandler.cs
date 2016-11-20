﻿namespace Warehouse.Domain.Commands
{
    public interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface ICommandHandler { }
}