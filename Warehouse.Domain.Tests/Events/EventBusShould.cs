using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.Events;

namespace Warehouse.Domain.Tests.Events
{
    [TestFixture]
    public class EventBusShould
    {
        [Test]
        public void ThrowsEventBusExcetionWhenPublishAndNoHandlingAction()
        {
            var eventBus = new EventBus();
            Check.ThatCode(() => eventBus.Publish(new Event1Fake())).Throws<EventBusException>();
        }

        [Test]
        public void ThrowsEventBusExcetionWhenRegisterNullEventHandler()
        {
            var eventBus = new EventBus();
            Check.ThatCode(() => eventBus.Register((IEventHandler<Event1Fake>)null)).Throws<EventBusException>();
        }

        [Test]
        public void ExecuteHandlingActionsWhenPublishEvent()
        {
            var handler1Mock = new Mock<IEventHandler<Event1Fake>>();
            var handler2Mock = new Mock<IEventHandler<Event1Fake>>();
            var handler3Mock = new Mock<IEventHandler<Event2Fake>>();
            var eventBus = new EventBus();

            eventBus.Register(handler1Mock.Object);
            eventBus.Register(handler2Mock.Object);
            eventBus.Register(handler3Mock.Object);
            eventBus.Publish(new Event1Fake());

            handler1Mock.Verify(x => x.Handle(It.IsAny<Event1Fake>()), Times.Once);
            handler2Mock.Verify(x => x.Handle(It.IsAny<Event1Fake>()), Times.Once);
            handler3Mock.Verify(x => x.Handle(It.IsAny<Event2Fake>()), Times.Never);
        }
    }

    public class Event1Fake : Event
    {
    }

    public class Event2Fake : Event
    {
    }
}
