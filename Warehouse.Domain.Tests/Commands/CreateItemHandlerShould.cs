using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.Commands;
using Warehouse.Domain.Commands.Exceptions;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;

namespace Warehouse.Domain.Tests.Commands
{
    [TestFixture]
    public class CreateItemHandlerShould
    {
        private Mock<IEventStore> eventStoreMock;

        private CreateItemHandler createItemHandler;

        [SetUp]
        public void Init()
        {
            this.eventStoreMock = new Mock<IEventStore>();
            this.createItemHandler = new CreateItemHandler(this.eventStoreMock.Object);
        }

        [Test]
        public void SaveItemCreatedInEventStoreWhenHandleCreateItemCommand()
        {
            this.createItemHandler.Handle(new CreateItemCommand("item name"));
            this.eventStoreMock.Verify(x => x.Save(It.Is<ItemCreated>(i => i.ItemName == "item name")));
        }

        [Test]
        [TestCase("")]
        [TestCase((string)null)]
        public void ThrowsCommandHandlerExceptionAndNotSaveEventToEventStoreWhenHandleCreateItemCommanWithEmptyItemName(string itemName)
        {
            Check.ThatCode(() => this.createItemHandler.Handle(new CreateItemCommand(itemName))).Throws<CommandHandlerException>();
        }
    }
}
