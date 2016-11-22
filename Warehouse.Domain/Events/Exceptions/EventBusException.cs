using System;

namespace Warehouse.Domain.Events.Exceptions
{
    public class EventBusException : Exception
    {
        public EventBusException(string message)
            : base (message)
        {
        }
    }
}