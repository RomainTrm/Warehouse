using System;
using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Domain.Repositories;

namespace Warehouse.Domain.Tests.Domain
{
    [TestFixture]
    public class ItemsListShould
    {
        [Test]
        public void ReturnItemsFormItemsListRepository()
        {
            var items = new[] { new Item(Guid.NewGuid(), "item1"), new Item(Guid.NewGuid(), "item2") };
            var repositoryMock = new Mock<IItemsListRepository>();
            repositoryMock.Setup(x => x.GetItems()).Returns(items);

            Check.That(new ItemsList(repositoryMock.Object).Items).Equals(items);
        }
    }
}
