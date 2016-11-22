using System;
using System.Collections;
using System.Collections.Generic;
using Warehouse.Domain.ReadModels.Base;

namespace Warehouse.DataAccess.ReadModels
{
    public class GenericRepository : IRepository
    {
        private readonly Dictionary<Type, IEnumerable> readModels = new Dictionary<Type, IEnumerable>(); 

        public void Insert<TData>(TData data)
        {
            if (!this.readModels.ContainsKey(typeof (TData)))
            {
                this.readModels[typeof(TData)] = new List<TData>();
            }

            var datas = this.GetDatas<TData>();
            datas.Add(data);
        }

        public void Update<TData>(TData data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TData> Get<TData>()
        {
            return this.GetDatas<TData>();
        }

        private List<TData> GetDatas<TData>()
        {
            return (List<TData>)this.readModels[typeof (TData)];
        }
    }
}
