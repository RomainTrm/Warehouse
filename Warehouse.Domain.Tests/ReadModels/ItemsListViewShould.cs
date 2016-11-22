using System;
using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.ReadModels;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.Tests.ReadModels
{
    [TestFixture]
    public class ItemsListViewShould
    {
        [Test]
        public void ReturnItemsFormItemsListRepository()
        {
            var items = new[] { new ItemView(Guid.NewGuid(), "item1"), new ItemView(Guid.NewGuid(), "item2") };
            var repositoryMock = new Mock<IItemsListRepository>();
            repositoryMock.Setup(x => x.GetItems()).Returns(items);

            Check.That(new ItemsListView(repositoryMock.Object).Items).Equals(items);
        }
    }
}
