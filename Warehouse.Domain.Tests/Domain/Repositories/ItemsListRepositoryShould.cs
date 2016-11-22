using System;
using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Domain.Base;
using Warehouse.Domain.Domain.Repositories;
using Warehouse.Domain.Events;

namespace Warehouse.Domain.Tests.Domain.Repositories
{
    [TestFixture]
    public class ItemsListRepositoryShould
    {
        private Mock<IRepository> repositoryMock;

        private ItemsListRepository itemsListRepository;

        [SetUp]
        public void Init()
        {
            this.repositoryMock = new Mock<IRepository>();
            this.itemsListRepository = new ItemsListRepository(this.repositoryMock.Object);
        }

        [Test]
        public void RetriveItemsWhenGetItems()
        {
            var items = new[] { new Item(Guid.NewGuid(), "item1"), new Item(Guid.NewGuid(), "item2") };
            this.repositoryMock.Setup(x => x.Get<Item>()).Returns(items);

            Check.That(this.itemsListRepository.GetItems()).Equals(items);
        }

        [Test]
        public void InsertNewItemWhenHandlerItemCreatedEvent()
        {
            var @event = new ItemCreated("item name");
            this.itemsListRepository.Handle(@event);
            this.repositoryMock.Verify(x => x.Insert(new Item(@event.ItemId, @event.ItemName)));
        }
    }
}
