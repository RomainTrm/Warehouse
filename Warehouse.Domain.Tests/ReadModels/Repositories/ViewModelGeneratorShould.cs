using System;
using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.Events;
using Warehouse.Domain.ReadModels;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.Tests.ReadModels.Repositories
{
    [TestFixture]
    public class ViewModelGeneratorShould
    {
        private Mock<IReadModelRepository> repositoryMock;

        private ViewModelGenerator viewModelGenerator;

        [SetUp]
        public void Init()
        {
            this.repositoryMock = new Mock<IReadModelRepository>();
            this.viewModelGenerator = new ViewModelGenerator(this.repositoryMock.Object);
        }

        [Test]
        public void InsertNewItemViewWhenHandleItemCreatedEvent()
        {
            var @event = new ItemCreated("item name");
            this.viewModelGenerator.Handle(@event);
            this.repositoryMock.Verify(x => x.Insert(new ItemView(@event.Id, @event.Name)));
        }

        [Test]
        public void ChangeItemViewNameAndUpdateWhenHandleItemRenamed()
        {
            var item = new ItemView(Guid.NewGuid(), "first name");
            this.repositoryMock.Setup(x => x.Get<ItemView>()).Returns(new[] { item });

            this.viewModelGenerator.Handle(new ItemRenamed(item.Id.Value, "new name"));

            Check.That(item.Name).Equals("new name");
            this.repositoryMock.Verify(x => x.Update(item));
        }

        [Test]
        public void RemoveItemAndAddItemToDisableItemsWhenHandlerItemDisabled()
        {
            var item = new ItemView(Guid.NewGuid(), "first name");
            this.repositoryMock.Setup(x => x.Get<ItemView>()).Returns(new[] {item});

            this.viewModelGenerator.Handle(new ItemDisabled(item.Id.Value));

            this.repositoryMock.Verify(x => x.Delete(item));
            this.repositoryMock.Verify(x => x.Insert(new DisableItemView(item.Id.Value, "first name")));
        }

        [Test]
        public void RemoveDisableItemAndAddItemToItemsWhenHandlerItemEnabled()
        {
            var disableItem = new DisableItemView(Guid.NewGuid(), "first name");
            this.repositoryMock.Setup(x => x.Get<DisableItemView>()).Returns(new[] { disableItem });

            this.viewModelGenerator.Handle(new ItemEnabled(disableItem.Id.Value));

            this.repositoryMock.Verify(x => x.Delete(disableItem));
            this.repositoryMock.Verify(x => x.Insert(new ItemView(disableItem.Id.Value, "first name")));
        }

        [Test]
        public void ChangeItemViewUnitsQuantityAndUpdateWhenHandleUnitsAdded()
        {
            var item = new ItemView(Guid.NewGuid(), "first name") { Units = 5 };
            this.repositoryMock.Setup(x => x.Get<ItemView>()).Returns(new[] { item });

            this.viewModelGenerator.Handle(new UnitsAdded(item.Id.Value, 9));

            Check.That(item.Units).Equals((uint) 14);
            this.repositoryMock.Verify(x => x.Update(item));
        }

        [Test]
        public void ChangeItemViewUnitsQuantityAndUpdateWhenHandleUnitsRemoved()
        {
            var item = new ItemView(Guid.NewGuid(), "first name") { Units = 5 };
            this.repositoryMock.Setup(x => x.Get<ItemView>()).Returns(new[] { item });

            this.viewModelGenerator.Handle(new UnitsRemoved(item.Id.Value, 3));

            Check.That(item.Units).Equals((uint)2);
            this.repositoryMock.Verify(x => x.Update(item));
        }
    }
}
