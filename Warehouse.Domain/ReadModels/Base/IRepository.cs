using System.Collections.Generic;

namespace Warehouse.Domain.ReadModels.Base
{
    public interface IRepository
    {
        void Insert<TData>(TData data);

        void Update<TData>(TData data);

        IEnumerable<TData> Get<TData>();
    }
}
