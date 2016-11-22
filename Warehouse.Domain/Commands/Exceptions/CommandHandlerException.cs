using System;

namespace Warehouse.Domain.Commands.Exceptions
{
    public class CommandHandlerException : Exception
    {
        public CommandHandlerException(string message)
            : base (message)
        {
        }   
    }
}