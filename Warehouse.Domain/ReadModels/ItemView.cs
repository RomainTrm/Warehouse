using System;
using System.Collections.Generic;

namespace Warehouse.Domain.ReadModels
{
    public class ItemView : IReadModel
    {
        private readonly List<string> history;

        internal ItemView(Guid itemId, string itemName)
        {
            this.Id = new ItemId(itemId);
            this.Name = itemName;
            this.history = new List<string>();
        }

        public ItemId Id { get; }

        public string Name { get; }

        public uint Units { get; internal set; }

        public IEnumerable<string> History => this.history;

        internal void AddHistoryRow(string row)
        {
            this.history.Add(row);
        }

        public override bool Equals(object obj)
        {
            return obj is ItemView && this.Equals((ItemView)obj);
        }

        private bool Equals(ItemView other)
        {
            return this.Id.Equals(other.Id) && string.Equals(this.Name, other.Name) && this.Units == other.Units;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Id?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (this.Name?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (int)this.Units;
                return hashCode;
            }
        }
    }
}