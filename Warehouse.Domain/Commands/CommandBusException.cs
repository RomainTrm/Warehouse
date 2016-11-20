using System;

namespace Warehouse.Domain.Commands
{
    public class CommandBusException : Exception
    {
        public CommandBusException(string message)
            : base(message)
        {
        }
    }
}
