using System;

namespace Warehouse.Domain.ReadModels
{
    public class ItemView : IReadModel
    {
        public ItemView(Guid itemId, string itemName)
        {
            this.Id = new ItemId(itemId);
            this.Name = itemName;
        }

        public ItemId Id { get; }

        public string Name { get; }

        public uint Units { get; internal set; }

        public override bool Equals(object obj)
        {
            return obj is ItemView && this.Equals((ItemView) obj);
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
                hashCode = (hashCode*397) ^ (this.Name?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (int) this.Units;
                return hashCode;
            }
        }
    }
}