using System;
using Warehouse.Domain.Commands.AddUnits;
using Warehouse.Domain.Commands.Bus;
using Warehouse.Domain.Commands.CreateItem;
using Warehouse.Domain.Commands.DisableItem;
using Warehouse.Domain.Commands.EnableItem;
using Warehouse.Domain.Commands.RemoveUnits;
using Warehouse.Domain.Commands.RenameItem;
using Warehouse.Domain.Events;
using Warehouse.Domain.Events.Base;
using Warehouse.Domain.Events.Bus;
using Warehouse.Domain.ReadModels.Repositories;

namespace Warehouse.Domain
{
    public static class Bootstrapper
    {
        public static void Init(
            IEventStoreRegistration eventStore, 
            IReadModelRepository readModelRepository)
        {
            if (ReadModelRepository != null)
            {
                throw new InvalidOperationException("Bootstrapper is already initialized.");
            }

            ReadModelRepository = readModelRepository;

            var eventBus = new EventBus();
            eventStore.SetEventBusToPublish(eventBus);
            var viewModelGenerator = new ViewModelGenerator(readModelRepository);

            eventBus.Register<ItemCreated>(viewModelGenerator);
            eventBus.Register<ItemRenamed>(viewModelGenerator);
            eventBus.Register<UnitsAdded>(viewModelGenerator);
            eventBus.Register<UnitsRemoved>(viewModelGenerator);
            eventBus.Register<ItemDisabled>(viewModelGenerator);
            eventBus.Register<ItemEnabled>(viewModelGenerator);

            var commandBus = new CommandBus();
            commandBus.RegisterHandler(new CreateItemHandler(eventStore));
            commandBus.RegisterHandler(new RenameItemHandler(eventStore));
            commandBus.RegisterHandler(new AddUnitsHandler(eventStore));
            commandBus.RegisterHandler(new RemoveUnitsHandler(eventStore));
            commandBus.RegisterHandler(new DisableItemHandler(eventStore));
            commandBus.RegisterHandler(new EnableItemHandler(eventStore));
            CommandBus = commandBus;
        }

        public static ICommandBus CommandBus { get; private set; }

        internal static IReadModelReadOnlyRepository ReadModelRepository { get; private set; }
    }
}
