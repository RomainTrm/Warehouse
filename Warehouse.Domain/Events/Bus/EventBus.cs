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
            where TEvent : IEvent
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
        
        public void Register<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent
        {
            if (eventHandler == null)
            {
                throw new EventBusException("You can't register a null handler.");
            }

            if (!this.handlers.Contains(eventHandler))
            {
                this.handlers.Add(eventHandler); 
            }
        }
    }
}