using System;
using System.Collections.Generic;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Domain
{
    internal class Item : Aggregate
    {
        public Item(IEnumerable<Event> events)
            : base(events)
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public bool IsEnabled { get; private set; }

        public uint Units { get; private set; }

        protected override IReadOnlyDictionary<Type, Action<Event>> GetAggregatedEvents()
        {
            return new Dictionary<Type, Action<Event>>
            {
                { typeof(ItemCreated), e => this.ApplyItemCreated((ItemCreated)e) },
                { typeof(ItemRenamed), e => this.ApplyItemRenamed((ItemRenamed)e) },
                { typeof(ItemDisabled), e => this.ApplyItemDisabled() },
                { typeof(ItemEnabled), e => this.ApplyItemEnabled() },
                { typeof(UnitsAdded), e => this.ApplyUnitsAdded((UnitsAdded)e) },
                { typeof(UnitsRemoved), e => this.ApplyUnitsRemoved((UnitsRemoved)e) }
            };
        }

        private void ApplyItemCreated(ItemCreated itemCreated)
        {
            this.Id = itemCreated.Id;
            this.Name = itemCreated.Name;
            this.IsEnabled = true;
        }

        private void ApplyItemRenamed(ItemRenamed itemRenamed)
        {
            this.Name = itemRenamed.NewName;
        }

        private void ApplyItemDisabled()
        {
            this.IsEnabled = false;
        }

        private void ApplyItemEnabled()
        {
            this.IsEnabled = true;
        }

        private void ApplyUnitsAdded(UnitsAdded unitsAdded)
        {
            this.Units += unitsAdded.Units;
        }

        private void ApplyUnitsRemoved(UnitsRemoved unitsRemoved)
        {
            this.Units -= unitsRemoved.Units;
        }

        private bool IsDisabled()
        {
            return !this.IsEnabled;
        }

        public void Rename(string newName)
        {
            if (this.IsEnabled)
            {
                throw new DomainException("You can't rename an enabled item.");
            }

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
            if (this.IsDisabled())
            {
                throw new DomainException("You can't add units to a disabled item.");
            }

            var @event = new UnitsAdded(this.Id, units);
            this.UncommitedEventsList.Add(@event);
            this.ApplyUnitsAdded(@event);
        }

        public void RemoveUnits(uint units)
        {
            if (this.IsDisabled())
            {
                throw new DomainException("You can't remove units to a disabled item.");
            }

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
            if (this.IsDisabled())
            {
                throw new DomainException("You can't disable an item when it's already disable.");
            }

            if (this.Units > 0)
            {
                throw new DomainException("You can't disable an item when it's have some units.");
            }

            var @event = new ItemDisabled(this.Id);
            this.ApplyItemDisabled();
            this.UncommitedEventsList.Add(@event);
        }

        public void Enable()
        {
            if (this.IsEnabled)
            {
                throw new DomainException("You can't enble an item when it's already enable.");
            }

            var @event = new ItemEnabled(this.Id);
            this.ApplyItemEnabled();
            this.UncommitedEventsList.Add(@event);
        }
    }
}