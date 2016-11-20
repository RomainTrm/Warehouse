namespace Warehouse.DataAccess.Events
{
    internal interface IEventStoreAccess
    {
        void Push(EventContainer eventContainer);
    }
}