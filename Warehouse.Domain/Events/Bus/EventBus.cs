using System;
using System.Collections.Generic;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.Events.Exceptions;

namespace Warehouse.Domain.Events.Bus
{
    internal class EventBus : IEventBus, IEventBusRegister
    {
        private readonly Dictionary<Type, List<Action<Event>>> handlers = new Dictionary<Type, List<Action<Event>>>();

        public void Publish(Event @event)
        {
            var eventType = @event.GetType();
            if (!this.handlers.ContainsKey(eventType))
            {
                throw new EventBusException($"There's no handler registered for command type {eventType}"); 
            }

            foreach (var eventHandler in this.handlers[eventType])
            {
                eventHandler.Invoke(@event);
            }
        }
        
        public void Register<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : Event
        {
            if (eventHandler == null)
            {
                throw new EventBusException("You can't register a null handler.");
            }

            var eventType = typeof (TEvent);
            if (!this.handlers.ContainsKey(eventType))
            {
                this.handlers[eventType] = new List<Action<Event>>();
            }

            this.handlers[eventType].Add(@event => eventHandler.Handle((TEvent)@event));
        }
    }
}