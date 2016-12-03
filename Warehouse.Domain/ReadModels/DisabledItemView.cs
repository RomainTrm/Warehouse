using System;

namespace Warehouse.Domain.ReadModels
{
    public class DisabledItemView : IReadModel
    {
        internal DisabledItemView(Guid id, string name)
        {
            this.Id = new ItemId(id);
            this.Name = name;
        }

        public ItemId Id { get; }

        public string Name { get; internal set; }

        public override bool Equals(object obj)
        {
            return obj is DisabledItemView && this.Equals((DisabledItemView) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Id?.GetHashCode() ?? 0)*397) ^ (this.Name?.GetHashCode() ?? 0);
            }
        }

        private bool Equals(DisabledItemView other)
        {
            return Equals(this.Id, other.Id) && string.Equals(this.Name, other.Name);
        }
    }
}
