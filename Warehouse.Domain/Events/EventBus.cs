using System;
using System.Collections.Generic;
using System.Linq;

namespace Warehouse.Domain.Events
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
                throw new EventBusException($"Aucun handler pour l'event {@event.GetType()}"); 
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
                throw new EventBusException("Vous ne pouvez pas enregister un handler null.");
            }

            this.handlers.Add(eventHandler);
        }
    }
}