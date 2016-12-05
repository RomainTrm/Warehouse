using System;

namespace Warehouse.Domain
{
    public class ItemId
    {
        internal ItemId(Guid idValue)
        {
            this.Value = idValue;
        }

        public Guid Value { get; }
        
        public override bool Equals(object obj)
        {
            return obj is ItemId && this.Equals((ItemId)obj);
        }

        private bool Equals(ItemId other)
        {
            return this.Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
