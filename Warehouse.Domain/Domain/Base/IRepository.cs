using System.Collections.Generic;

namespace Warehouse.Domain.Domain.Base
{
    public interface IRepository
    {
        void Insert<TData>(TData data);

        void Update<TData>(TData data);

        IEnumerable<TData> Get<TData>();
    }
}
