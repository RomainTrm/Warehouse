using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Warehouse.Domain.Commands.DisableItem;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Tests.Commands
{
    [TestFixture]
    public class DisableItemHandlerShould
    {
        private readonly ItemCreated itemCreated = new ItemCreated("item name");

        private Mock<IEventStore> eventStoreMock;

        private DisableItemHandler disableItemHandler;

        [SetUp]
        public void Init()
        {
            this.eventStoreMock = new Mock<IEventStore>();
            this.eventStoreMock.Setup(x => x.GetEventsById(this.itemCreated.Id)).Returns(new[] { this.itemCreated });
            this.disableItemHandler = new DisableItemHandler(this.eventStoreMock.Object);
        }

        [Test]
        public void SaveItemDisabledToEventStore()
        {
            this.disableItemHandler.Handle(new DisableItemCommand(new ItemId(this.itemCreated.Id)));
            this.eventStoreMock.Verify(x => x.Save(It.Is<IEnumerable<Event>>(events => events.Single() is ItemDisabled && events.Single().Id == this.itemCreated.Id)));
        }
    }
}
