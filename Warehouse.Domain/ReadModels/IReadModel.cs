using System;

namespace Warehouse.Domain.ReadModels
{
    public interface IReadModel
    {
        Guid Id { get; }
    }
}