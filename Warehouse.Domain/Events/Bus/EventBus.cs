using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.Events.Exceptions;

namespace Warehouse.Domain.Events.Bus
{
    public class EventBus : IEventBus, IEventBusRegister
    {
        private readonly List<IEventHandler> handlers = new List<IEventHandler>();

        public void Publish<TEvent>(TEvent @event)
            where TEvent : Event
        {
            var eventHandlers = this.handlers.OfType<IEventHandler<TEvent>>().ToArray();
            if (!eventHandlers.Any())
            {
                throw new EventBusException($"There's no handler registered for command type {@event.GetType()}"); 
            }

            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Handle(@event);
            }
        }
        
        public void Register<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : Event
        {
            if (eventHandler == null)
            {
                throw new EventBusException("You can't register a null handler.");
            }

            this.handlers.Add(eventHandler);
        }
    }
}