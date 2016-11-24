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

        public string Name { get; internal set; }

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