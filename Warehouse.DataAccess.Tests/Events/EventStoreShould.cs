using System;
using Moq;
using NUnit.Framework;
using Warehouse.DataAccess.Events;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.DataAccess.Tests.Events
{
    [TestFixture]
    public class EventStoreShould
    {
        private readonly DateTime dateTime = DateTime.Now;

        private Mock<IEventStoreAccess> eventStoreAccessMock;
        private EventStore eventStore;

        [SetUp]
        public void Init()
        {
            this.eventStoreAccessMock = new Mock<IEventStoreAccess>();

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.GetDateTime()).Returns(this.dateTime);

            this.eventStore = new EventStore(this.eventStoreAccessMock.Object, dateTimeProviderMock.Object);
        }

        [Test]
        public void SaveEventInContainerWithActualDateTime()
        {
            var fakeEvent = new FakeEvent(1, 2);

            this.eventStore.Save(fakeEvent);

            this.eventStoreAccessMock.Verify(x => x.Push(new EventContainer(fakeEvent, this.dateTime)));
        }
    }

    public class FakeEvent : Event
    {
        public FakeEvent(int value1, int value2)
        {
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public int Value1 { get; }
        public int Value2 { get; }
    }
}
