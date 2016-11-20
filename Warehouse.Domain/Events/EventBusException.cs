using System;

namespace Warehouse.Domain.Events
{
    public class EventBusException : Exception
    {
        public EventBusException(string message)
            : base (message)
        {
        }
    }
}