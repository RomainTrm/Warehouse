using System;
using System.Linq;
using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.ReadModels;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.Tests.ReadModels
{
    [TestFixture]
    public class DisableItemsListViewShould
    {
        [Test]
        public void ReturnDisableItemsFromReadModelRepository()
        {
            var itemId = Guid.NewGuid();
            var repositoryMock = new Mock<IReadModelReadOnlyRepository>();
            repositoryMock.Setup(x => x.Get<DisabledItemView>()).Returns(new[] { new DisabledItemView(itemId, "chair") });

            var disableItemsListView = new DisabledItemsListView(repositoryMock.Object);
            Check.That(disableItemsListView.Items).ContainsExactly(new DisabledItemView(itemId, "chair"));
        }

        [Test]
        public void ReturnAnItemFromIsId()
        {
            var items = new[] { new DisabledItemView(Guid.NewGuid(), "item1"), new DisabledItemView(Guid.NewGuid(), "item2"), new DisabledItemView(Guid.NewGuid(), "item3") };
            var repositoryMock = new Mock<IReadModelReadOnlyRepository>();
            repositoryMock.Setup(x => x.Get<DisabledItemView>()).Returns(items);

            Check.That(new DisabledItemsListView(repositoryMock.Object).GetItem(items.First().Id.Value)).Equals(items.First());
        }
    }
}
