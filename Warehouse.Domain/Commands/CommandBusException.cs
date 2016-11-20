using System;

namespace Warehouse.Domain.Commands
{
    public class CommandBusException : Exception
    {
        public CommandBusException()
        {
        }

        public CommandBusException(string message)
            : base(message)
        {
        }

        public CommandBusException(string message, Exception innerException)
            : base (message, innerException)
        {
        }
    }
}
