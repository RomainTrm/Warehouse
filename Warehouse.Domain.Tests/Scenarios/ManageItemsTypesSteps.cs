using System;
using System.Linq;
using NFluent;
using TechTalk.SpecFlow;
using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Commands.Bus;
using Warehouse.Domain.Commands.CreateItem;
using Warehouse.Domain.Commands.RenameItem;
using Warehouse.Domain.Domain;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.Events.Bus;
using Warehouse.Domain.ReadModels;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain.Tests.Scenarios
{
    [Binding]
    [Scope(Feature = "ManageItemsTypes")]
    public class ManageItemsTypesSteps
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

        private IItemsListRepository ItemsListRepository
        {
            get { return (IItemsListRepository)ScenarioContext.Current["ItemsListRepository"]; }
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
            var itemListRepository = new ItemsListRepository(new GenericRepositoryFake());
            this.ItemsListRepository = itemListRepository;

            var eventBus = new EventBus();
            eventBus.Register<ItemCreated>(itemListRepository);
            eventBus.Register<ItemRenamed>(itemListRepository);

            this.EventStore = new EventStoreFake(eventBus);

            var commandBus = new CommandBus();
            this.CommandBus = commandBus;
            commandBus.RegsiterHandler(new CreateItemHandler(this.EventStore));
            commandBus.RegsiterHandler(new RenameItemHandler(this.EventStore));
        }

        [Given(@"I created an item ""(.*)""")]
        public void GivenICreatedAnItem(string itemName)
        {
            var itemCreated = new ItemCreated(itemName);
            this.ItemId = itemCreated.Id;
            this.EventStore.Save(itemCreated);
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

        [Then(@"Rename fail")]
        public void ThenRenameFail()
        {
            Check.That(this.CatchedException).IsNotNull();
        }

        [Then(@"I can see ""(.*)"" item in my items list")]
        public void ThenICanSeeItemInMyItemsList(string itemName)
        {
            var items = new ItemsListView(this.ItemsListRepository).Items;
            Check.That(items.Single().Name).Equals(itemName);
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
