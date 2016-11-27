using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Warehouse.Domain.Commands.RemoveUnits;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Tests.Commands
{
    [TestFixture]
    public class RemoveUnitsHandlerShould
    {
        private readonly ItemCreated itemCreated = new ItemCreated("item name");

        private Mock<IEventStore> eventStoreMock;

        private RemoveUnitsHandler removeUnitsHandler;

        [SetUp]
        public void Init()
        {
            this.eventStoreMock = new Mock<IEventStore>();
            this.eventStoreMock.Setup(x => x.GetEventsById(this.itemCreated.Id)).Returns(new Event[]
            {
                this.itemCreated, new UnitsAdded(this.itemCreated.Id, 9) 
            });
            this.removeUnitsHandler = new RemoveUnitsHandler(this.eventStoreMock.Object);
        }

        [Test]
        public void SaveUnitsRemovedToEventStore()
        {
            const uint units = 6;
            this.removeUnitsHandler.Handle(new RemoveUnitsCommand(new ItemId(this.itemCreated.Id), units));
            this.eventStoreMock.Verify(x => x.Save(It.Is<IEnumerable<Event>>(e => e.Single().Id == this.itemCreated.Id
                                                                               && ((UnitsRemoved)e.Single()).Units == units)));
        }
    }
}
