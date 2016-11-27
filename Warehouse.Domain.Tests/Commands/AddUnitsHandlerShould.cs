using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Warehouse.Domain.Commands.AddUnits;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Tests.Commands
{
    [TestFixture]
    public class AddUnitsHandlerShould
    {
        private readonly ItemCreated itemCreated = new ItemCreated("item name");

        private Mock<IEventStore> eventStoreMock;

        private AddUnitsHandler addUnitsHandler;

        [SetUp]
        public void Init()
        {
            this.eventStoreMock = new Mock<IEventStore>();
            this.eventStoreMock.Setup(x => x.GetEventsById(this.itemCreated.Id)).Returns(new[] { this.itemCreated });
            this.addUnitsHandler = new AddUnitsHandler(this.eventStoreMock.Object);
        }

        [Test]
        public void SaveUnitsAddedToEventStore()
        {
            const uint units = 6;
            this.addUnitsHandler.Handle(new AddUnitsCommand(new ItemId(this.itemCreated.Id), units));
            this.eventStoreMock.Verify(x => x.Save(It.Is<IEnumerable<Event>>(e => e.Single().Id == this.itemCreated.Id
                                                                               && ((UnitsAdded)e.Single()).Units == units)));
        }
    }
}
