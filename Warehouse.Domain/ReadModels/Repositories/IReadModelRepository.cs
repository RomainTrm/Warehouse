namespace Warehouse.Domain.ReadModels.Repositories
{
    public interface IReadModelRepository : IReadModelReadOnlyRepository
    {
        void Insert<TData>(TData data) where TData : IReadModel;

        void Update<TData>(TData data) where TData : IReadModel;

        void Delete<TData>(TData data) where TData : IReadModel;
    }
}
