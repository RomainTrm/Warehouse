using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Warehouse.Domain.Commands.EnableItem;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Tests.Commands
{
    [TestFixture]
    public class EnableItemHandlerShould
    {
        private readonly ItemCreated itemCreated = new ItemCreated("item name");

        private Mock<IEventStore> eventStoreMock;

        private EnableItemHandler enableItemHandler;

        [SetUp]
        public void Init()
        {
            this.eventStoreMock = new Mock<IEventStore>();
            this.eventStoreMock.Setup(x => x.GetEventsById(this.itemCreated.Id)).Returns(new Event[] { this.itemCreated, new ItemDisabled(this.itemCreated.Id) });
            this.enableItemHandler = new EnableItemHandler(this.eventStoreMock.Object);
        }

        [Test]
        public void SaveItemEnabledToEventStore()
        {
            this.enableItemHandler.Handle(new EnableItemCommand(new ItemId(this.itemCreated.Id)));
            this.eventStoreMock.Verify(x => x.Save(It.Is<IEnumerable<Event>>(events => events.Single() is ItemEnabled && events.Single().Id == this.itemCreated.Id)));
        }
    }
}