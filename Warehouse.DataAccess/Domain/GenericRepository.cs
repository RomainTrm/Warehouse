using System;
using System.Collections.Generic;
using Warehouse.Domain.Domain.Base;

namespace Warehouse.DataAccess.Domain
{
    public class GenericRepository : IRepository
    {
        public void Insert<TData>(TData data)
        {
            throw new NotImplementedException();
        }

        public void Update<TData>(TData data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TData> Get<TData>()
        {
            throw new NotImplementedException();
        }
    }
}
