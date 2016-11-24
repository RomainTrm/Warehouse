using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.Events.Bus;
using Warehouse.Domain.Events.Exceptions;

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

        [Test]
        public void NotPublishTwiceWhenAnHandlerIsRegisteredTwiceForTwoDifferentEventsTypes()
        {
            var handler1Mock = new Mock<IEventHandler<Event1Fake>>();
            var handler2Mock = new Mock<IEventHandler<Event2Fake>>();
            var eventHandlerFake = new EventHandlerFake(handler1Mock, handler2Mock);

            var eventBus = new EventBus();
            eventBus.Register<Event1Fake>(eventHandlerFake);
            eventBus.Register<Event2Fake>(eventHandlerFake);
            eventBus.Publish(new Event1Fake());

            handler1Mock.Verify(x => x.Handle(It.IsAny<Event1Fake>()), Times.Once);
            handler2Mock.Verify(x => x.Handle(It.IsAny<Event2Fake>()), Times.Never);
        }
    }

    public class Event1Fake : Event
    {
    }

    public class Event2Fake : Event
    {
    }

    public class EventHandlerFake : IEventHandler<Event1Fake>, IEventHandler<Event2Fake>
    {
        private readonly Mock<IEventHandler<Event1Fake>> mockHandler1;
        private readonly Mock<IEventHandler<Event2Fake>> mockHandler2;

        public EventHandlerFake(Mock<IEventHandler<Event1Fake>> mockHandler1, Mock<IEventHandler<Event2Fake>> mockHandler2)
        {
            this.mockHandler1 = mockHandler1;
            this.mockHandler2 = mockHandler2;
        }

        public void Handle(Event1Fake @event)
        {
            this.mockHandler1.Object.Handle(@event);
        }

        public void Handle(Event2Fake @event)
        {
            this.mockHandler2.Object.Handle(@event);
        }
    }
}
