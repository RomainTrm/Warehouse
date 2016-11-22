using System;

namespace Warehouse.Domain.ReadModels
{
    public class ItemView
    {
        public ItemView(Guid itemId, string itemName)
        {
            this.Id = itemId;
            this.Name = itemName;
        }

        public Guid Id { get; }

        public string Name { get; }

        public override bool Equals(object obj)
        {
            return obj is ItemView && this.Equals((ItemView) obj);
        }

        private bool Equals(ItemView other)
        {
            return this.Id.Equals(other.Id) && string.Equals(this.Name, other.Name);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Id.GetHashCode()*397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
            }
        }
    }
}