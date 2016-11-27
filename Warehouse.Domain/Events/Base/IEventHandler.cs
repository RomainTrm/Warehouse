namespace Warehouse.Domain.Events.Base
{
    public interface IEventHandler<in TEvent>
        where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
