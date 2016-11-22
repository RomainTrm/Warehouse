using Moq;
using NUnit.Framework;
using Warehouse.Domain.Commands.RenameItem;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Tests.Commands
{
    [TestFixture]
    public class RenameItemHandlerShould
    {
        private readonly ItemCreated itemCreated = new ItemCreated("item name");

        private Mock<IEventStore> eventStoreMock;

        private RenameItemHandler renameItemHandler;

        [SetUp]
        public void Init()
        {
            this.eventStoreMock = new Mock<IEventStore>();
            this.eventStoreMock.Setup(x => x.GetEventsById(this.itemCreated.Id)).Returns(new[] { this.itemCreated });
            this.renameItemHandler = new RenameItemHandler(this.eventStoreMock.Object);
        }

        [Test]
        public void SaveItemRenamedToEventStore()
        {
            this.renameItemHandler.Handle(new RenameItemCommand(this.itemCreated.Id, "new name"));
            this.eventStoreMock.Verify(x => x.Save(It.Is<ItemRenamed>(e => e.Id == this.itemCreated.Id && e.NewName == "new name")));
        }
    }
}
