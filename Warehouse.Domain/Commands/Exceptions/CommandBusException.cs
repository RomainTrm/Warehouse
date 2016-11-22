using System;

namespace Warehouse.Domain.Commands.Exceptions
{
    public class CommandBusException : Exception
    {
        public CommandBusException(string message)
            : base(message)
        {
        }
    }
}
