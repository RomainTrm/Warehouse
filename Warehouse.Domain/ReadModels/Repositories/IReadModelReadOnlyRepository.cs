using System.Collections.Generic;

namespace Warehouse.Domain.ReadModels.Repositories
{
    public interface IReadModelReadOnlyRepository
    {
        IEnumerable<TData> Get<TData>() where TData : IReadModel;
    }
}