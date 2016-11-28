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

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public bool IsEnable { get; private set; }

        public uint Units { get; private set; }

        protected override IReadOnlyDictionary<Type, Action<Event>> GetAggregatedEvents()
        {
            return new Dictionary<Type, Action<Event>>
            {
                { typeof(ItemCreated), e => this.ApplyItemCreated((ItemCreated)e) },
                { typeof(ItemRenamed), e => this.ApplyItemRenamed((ItemRenamed)e) },
                { typeof(ItemDisabled), e => this.ApplyItemDisabled((ItemDisabled)e) },
                { typeof(UnitsAdded), e => this.ApplyUnitsAdded((UnitsAdded)e) },
                { typeof(UnitsRemoved), e => this.ApplyUnitsRemoved((UnitsRemoved)e) }
            };
        }

        private void ApplyItemCreated(ItemCreated itemCreated)
        {
            this.Id = itemCreated.Id;
            this.Name = itemCreated.Name;
            this.IsEnable = true;
        }

        private void ApplyItemRenamed(ItemRenamed itemRenamed)
        {
            this.Name = itemRenamed.NewName;
        }

        private void ApplyItemDisabled(ItemDisabled itemDisabled)
        {
            this.IsEnable = false;
	}

        private void ApplyUnitsAdded(UnitsAdded unitsAdded)
        {
            this.Units += unitsAdded.Units;
        }

        private void ApplyUnitsRemoved(UnitsRemoved unitsRemoved)
        {
            this.Units -= unitsRemoved.Units;
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new DomainException("You can't set an empty name to an item.");
            }

            var @event = new ItemRenamed(this.Id, newName);
            this.UncommitedEventsList.Add(@event);
            this.ApplyItemRenamed(@event);
        }

        public void AddUnits(uint units)
        {
            var @event = new UnitsAdded(this.Id, units);
            this.UncommitedEventsList.Add(@event);
            this.ApplyUnitsAdded(@event);
        }

        public void RemoveUnits(uint units)
        {
            if (this.Units < units)
            {
                throw new DomainException("You can't have a total of units lower than zero.");
            }

            var @event = new UnitsRemoved(this.Id, units);
            this.UncommitedEventsList.Add(@event);
            this.ApplyUnitsRemoved(@event);
        }

        public void Disable()
        {
	    var @event = new ItemDisabled(this.Id);
	    this.ApplyItemDisabled(@event);
            this.UncommitedEventsList.Add(@event);
        }
    }
}