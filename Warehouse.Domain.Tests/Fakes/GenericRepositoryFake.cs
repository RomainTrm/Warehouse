using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.ReadModels;
using Warehouse.Domain.ReadModels.Base;

namespace Warehouse.Domain.Tests.Fakes
{
    public class GenericRepositoryFake : IRepository
    {
        private readonly Dictionary<Type, IEnumerable> readModels = new Dictionary<Type, IEnumerable>(); 

        public void Insert<TData>(TData data)
             where TData : IReadModel
        {
            if (!this.readModels.ContainsKey(typeof (TData)))
            {
                this.readModels[typeof(TData)] = new List<TData>();
            }

            var datas = this.GetDatas<TData>();
            datas.Add(data);
        }

        public void Update<TData>(TData data)
             where TData : IReadModel
        {
            var datas = this.GetDatas<TData>();
            var itemIdex = datas.IndexOf(datas.Single(x => x.Id == data.Id));
            datas[itemIdex] = data;
        }

        public IEnumerable<TData> Get<TData>()
             where TData : IReadModel
        {
            return this.GetDatas<TData>();
        }

        private List<TData> GetDatas<TData>()
        {
            return (List<TData>)this.readModels[typeof (TData)];
        }
    }
}
