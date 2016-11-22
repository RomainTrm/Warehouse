using System;
using System.Collections.Generic;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Domain
{
    public class Item : Aggregate
    {
        public Item(IEnumerable<Event> events) 
            : base(events)
        {
        }

        protected override IReadOnlyDictionary<Type, Action<Event>> GetAggregatedEvents()
        {
            return new Dictionary<Type, Action<Event>>
            {
                { typeof(ItemCreated), e => this.ApplyItemCreated((ItemCreated)e) },
                { typeof(ItemRenamed), e => this.ApplayItemRenamed((ItemRenamed)e) }
            };
        }

        private void ApplyItemCreated(ItemCreated itemCreated)
        {
            this.Id = itemCreated.Id;
            this.Name = itemCreated.Name;
        }

        private void ApplayItemRenamed(ItemRenamed itemRenamed)
        {
            this.Name = itemRenamed.NewName;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }
    }
}