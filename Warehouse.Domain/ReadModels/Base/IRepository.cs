using System.Collections.Generic;

namespace Warehouse.Domain.ReadModels.Base
{
    public interface IRepository
    {
        void Insert<TData>(TData data) where TData : IReadModel;

        void Update<TData>(TData data) where TData : IReadModel;

        IEnumerable<TData> Get<TData>() where TData : IReadModel;
    }
}
