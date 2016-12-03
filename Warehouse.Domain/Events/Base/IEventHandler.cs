namespace Warehouse.Domain.Events.Base
{
    internal interface IEventHandler<in TEvent>
        where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
