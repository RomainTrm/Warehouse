using System;
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
    }
}
