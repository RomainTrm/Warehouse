using System;
using Warehouse.Domain.ReadModels;

namespace Warehouse.UI.Models
{
    public class ItemDetailsViewModel
    {
        public Guid Id { get; set; }

        public ItemView Item { get; set; }

        public uint Quantity { get; set; }
    }
}