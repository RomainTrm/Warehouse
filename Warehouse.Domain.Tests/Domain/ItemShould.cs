using System;
using System.Linq;
using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.Tests.Events;

namespace Warehouse.Domain.Tests.Domain
{
    [TestFixture]
    public class ItemShould
    {
        [Test]
        public void HaveCorrectIdAndNameWhenProvideItemCreated()
        {
            var itemCreated = new ItemCreated("item name");
            var item = new Item(new[] { itemCreated });

            Check.That(item.Id).Equals(itemCreated.Id);
            Check.That(item.Name).Equals("item name");
        }

        [Test]
        public void HaveCorrectIdAndNameWhenProvideItemCreatedAndItemRenamed()
        {
            var itemCreated = new ItemCreated("item name");

            var events = new Event[] {itemCreated, new ItemRenamed(It.IsAny<Guid>(), "new item name") };
            var item = new Item(events);

            Check.That(item.Id).Equals(itemCreated.Id);
            Check.That(item.Name).Equals("new item name");
        }

        [Test]
        public void IgnoreUnexpectedEvents()
        {
            var itemCreated = new ItemCreated("item name");
            var events = new Event[] { itemCreated, new ItemRenamed(It.IsAny<Guid>(), "new item name"), new Event1Fake() };
            Check.ThatCode(() => new Item(events)).DoesNotThrow();
        }

        [Test]
        public void AddToUncommitedEventsItemRenamedWhenRename()
        {
            var itemCreated = new ItemCreated("item name");
            var item = new Item(new[] { itemCreated });

            item.Rename("new name");

            Check.That(item.Name).Equals("new name");
            Check.That(item.UncommitedEvents.Single()).HasFieldsWithSameValues(new ItemRenamed(itemCreated.Id, "new name"));
        }

        [Test]
        [TestCase("")]
        [TestCase((string)null)]
        public void ThrowsDomainExceptionWhenRenameWithAnName(string newName)
        {
            var itemCreated = new ItemCreated("item name");
            var item = new Item(new[] { itemCreated });

            Check.ThatCode(() => item.Rename(newName)).Throws<DomainException>();
        }
    }
}
