using System;

namespace Warehouse.Domain.Domain
{
    public class Item
    {
        public Item(Guid itemId, string itemName)
        {
            this.Id = itemId;
            this.Name = itemName;
        }

        public Guid Id { get; }

        public string Name { get; }

        public override bool Equals(object obj)
        {
            return obj is Item && this.Equals((Item) obj);
        }

        private bool Equals(Item other)
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