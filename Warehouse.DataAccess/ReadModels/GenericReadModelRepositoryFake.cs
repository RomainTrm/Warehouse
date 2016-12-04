using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Domain.ReadModels;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.DataAccess.ReadModels
{
    public class GenericReadModelRepositoryFake : IReadModelRepository
    {
        private readonly Dictionary<Type, IEnumerable> readModels = new Dictionary<Type, IEnumerable>(); 

        public void Insert<TData>(TData data)
             where TData : IReadModel
        {
            var datas = this.GetDatas<TData>();
            datas.Add(data);
        }

        public void Update<TData>(TData data)
             where TData : IReadModel
        {
            var datas = this.GetDatas<TData>();
            var itemIdex = datas.IndexOf(datas.Single(x => Equals(x.Id, data.Id)));
            datas[itemIdex] = data;
        }

        public IEnumerable<TData> Get<TData>()
             where TData : IReadModel
        {
            return this.GetDatas<TData>();
        }

        public void Delete<TData>(TData data) where TData : IReadModel
        {
            var datas = this.GetDatas<TData>();
            var itemIdex = datas.IndexOf(datas.Single(x => Equals(x.Id, data.Id)));
            datas.RemoveAt(itemIdex);
        }

        private List<TData> GetDatas<TData>()
        {
            if (!this.readModels.ContainsKey(typeof (TData)))
            {
                this.readModels[typeof(TData)] = new List<TData>();
            }

            return (List<TData>)this.readModels[typeof (TData)];
        }
    }
}
