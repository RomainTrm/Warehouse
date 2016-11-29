using System;
using System.Linq;
using NFluent;
using TechTalk.SpecFlow;
using Warehouse.Domain.Commands.AddUnits;
using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Commands.Bus;
using Warehouse.Domain.Commands.CreateItem;
using Warehouse.Domain.Commands.DisableItem;
using Warehouse.Domain.Commands.EnableItem;
using Warehouse.Domain.Commands.RemoveUnits;
using Warehouse.Domain.Commands.RenameItem;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.Events.Bus;
using Warehouse.Domain.ReadModels;
using Warehouse.Domain.ReadModels.Repositories;
using Warehouse.Domain.Tests.Fakes;

namespace Warehouse.Domain.Tests
{
    [Binding]
    [Scope(Feature = "ItemsListView")]
    public class ItemsListViewSteps
    {
        private ICommandBus CommandBus
        {
            get { return (ICommandBus)ScenarioContext.Current["CommandBus"]; }
            set { ScenarioContext.Current["CommandBus"] = value; }
        }

        private IEventStore EventStore
        {
            get { return (IEventStore)ScenarioContext.Current["EventStore"]; }
            set { ScenarioContext.Current["EventStore"] = value; }
        }

        private IReadModelRepository ReadModelRepository
        {
            get { return (IReadModelRepository)ScenarioContext.Current["ItemsListRepository"]; }
            set { ScenarioContext.Current["ItemsListRepository"] = value; }
        }

        private Guid ItemId
        {
            get { return (Guid)ScenarioContext.Current["ItemId"]; }
            set { ScenarioContext.Current["ItemId"] = value; }
        }

        private DomainException CatchedException
        {
            get { return (DomainException)ScenarioContext.Current["CatchedException"]; }
            set { ScenarioContext.Current["CatchedException"] = value; }
        }

        [BeforeScenario]
        public void Init()
        {
            this.ReadModelRepository = new GenericReadModelRepositoryFake();
            var viewModelGenerator = new ViewModelGenerator(this.ReadModelRepository);

            var eventBus = new EventBus();
            eventBus.Register<ItemCreated>(viewModelGenerator);
            eventBus.Register<ItemRenamed>(viewModelGenerator);
            eventBus.Register<UnitsAdded>(viewModelGenerator);
            eventBus.Register<UnitsRemoved>(viewModelGenerator);
            eventBus.Register<ItemDisabled>(viewModelGenerator);
            eventBus.Register<ItemEnabled>(viewModelGenerator);
            this.EventStore = new EventStoreFake(eventBus);

            var commandBus = new CommandBus();
            commandBus.RegsiterHandler(new CreateItemHandler(this.EventStore));
            commandBus.RegsiterHandler(new RenameItemHandler(this.EventStore));
            commandBus.RegsiterHandler(new AddUnitsHandler(this.EventStore));
            commandBus.RegsiterHandler(new RemoveUnitsHandler(this.EventStore));
            commandBus.RegsiterHandler(new DisableItemHandler(this.EventStore));
            commandBus.RegsiterHandler(new EnableItemHandler(this.EventStore));
            this.CommandBus = commandBus;
        }

        [Given(@"I created an item ""(.*)""")]
        public void GivenICreatedAnItem(string itemName)
        {
            var itemCreated = new ItemCreated(itemName);
            this.ItemId = itemCreated.Id;
            this.EventStore.Save(itemCreated);
        }

        [Given(@"I added it (.*) units")]
        public void GivenIAddedItUnits(uint units)
        {
            this.EventStore.Save(new UnitsAdded(this.ItemId, units));
        }

        [Given(@"I disabled it")]
        public void GivenIDisabledIt()
        {
            this.EventStore.Save(new ItemDisabled(this.ItemId));
        }

        [When(@"I create a new item ""(.*)""")]
        public void WhenICreateANewItem(string itemName)
        {
            this.SendCommand(new CreateItemCommand(itemName));
        }

        [When(@"I rename it ""(.*)""")]
        public void WhenIRenameIt(string itemName)
        {
            this.SendCommand(new RenameItemCommand(new ItemId(this.ItemId), itemName));
        }

        [When(@"I add (.*) units")]
        public void WhenIAddUnits(uint units)
        {
            this.SendCommand(new AddUnitsCommand(new ItemId(this.ItemId), units));
        }

        [When(@"I remove (.*) units")]
        public void WhenIRemoveUnits(uint units)
        {
            this.SendCommand(new RemoveUnitsCommand(new ItemId(this.ItemId), units));
        }

        [When(@"I disable it")]
        public void WhenIDisableIt()
        {
            this.SendCommand(new DisableItemCommand(new ItemId(this.ItemId)));
        }
        
        [When(@"I enable it")]
        public void WhenIEnableIt()
        {
            this.SendCommand(new EnableItemCommand(new ItemId(this.ItemId)));
        }

        [Then(@"It fails")]
        public void ThenItFails()
        {
            Check.That(this.CatchedException).IsNotNull();
        }

        [Then(@"I can see ""(.*)"" item in my items list")]
        public void ThenICanSeeItemInMyItemsList(string itemName)
        {
            var items = new ItemsListView(this.ReadModelRepository).Items;
            Check.That(items.Single().Name).Equals(itemName);
        }

        [Then(@"I can see ""(.*)"" items with (.*) units in my items list")]
        public void ThenICanSeeItemsWithUnitsInMyItemsList(string itemName, uint units)
        {
            var items = new ItemsListView(this.ReadModelRepository).Items;
            Check.That(items.Single().Name).Equals(itemName);
            Check.That(items.Single().Units).Equals(units);
        }

        [Then(@"I can't see ""(.*)"" item in my items list")]
        public void ThenICanTSeeItemInMyItemsList(string itemName)
        {
            var items = new ItemsListView(this.ReadModelRepository).Items;
            Check.That(items).IsEmpty();
        }

        [Then(@"I can see ""(.*)"" item in my disable items list")]
        public void ThenICanSeeItemInMyDisableItemsList(string itemName)
        {
            var disableItems = new DisableItemsListView(this.ReadModelRepository).Items;
            Check.That(disableItems.Single().Name).Equals(itemName);
        }
        
        [Then(@"I can't see ""(.*)"" item in my disable items list")]
        public void ThenICanTSeeItemInMyDisableItemsList(string itemName)
        {
            var disableItems = new DisableItemsListView(this.ReadModelRepository).Items;
            Check.That(disableItems).IsEmpty();
        }

        private void SendCommand<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            try
            {
                this.CommandBus.Send(command);
            }
            catch (DomainException ex)
            {
                this.CatchedException = ex;
            }
        }
    }
}
