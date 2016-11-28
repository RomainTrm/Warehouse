using System.Collections.Generic;

namespace Warehouse.Domain.ReadModels.Repositories
{
    public interface IReadModelRepository
    {
        void Insert<TData>(TData data) where TData : IReadModel;

        void Update<TData>(TData data) where TData : IReadModel;

        IEnumerable<TData> Get<TData>() where TData : IReadModel;

        void Delete<TData>(TData data) where TData : IReadModel;
    }
}
