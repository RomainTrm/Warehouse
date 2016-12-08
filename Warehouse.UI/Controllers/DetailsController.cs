using System;
using System.Web.Mvc;
using Warehouse.Domain;
using Warehouse.Domain.Commands.AddUnits;
using Warehouse.Domain.Commands.Bus;
using Warehouse.Domain.Commands.DisableItem;
using Warehouse.Domain.Commands.RemoveUnits;
using Warehouse.Domain.ReadModels;
using Warehouse.UI.Models;

namespace Warehouse.UI.Controllers
{
    [HandleError]
    public class DetailsController : Controller
    {
        private readonly ICommandBus commandBus;

        public DetailsController()
        {
            this.commandBus = Bootstrapper.CommandBus;
        }

        [HttpPost]
        public ActionResult AddUnits(ItemDetailsViewModel itemDetailsViewModel)
        {
            try
            {
                var item = new ItemsListView().GetItem(itemDetailsViewModel.Id);
                this.commandBus.Send(new AddUnitsCommand(item.Id, itemDetailsViewModel.Quantity));
                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Error", "Error", new ErrorViewModel(ex.Message));
            }
        }

        [HttpPost]
        public ActionResult RemoveUnits(ItemDetailsViewModel itemDetailsViewModel)
        {
            try
            {
                var item = new ItemsListView().GetItem(itemDetailsViewModel.Id);
                this.commandBus.Send(new RemoveUnitsCommand(item.Id, itemDetailsViewModel.Quantity));
                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Error", "Error", new ErrorViewModel(ex.Message));
            }
        }

        public ActionResult Disable(Guid id)
        {
            try
            {
                var item = new ItemsListView().GetItem(id);
                this.commandBus.Send(new DisableItemCommand(item.Id));
                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Error", "Error", new ErrorViewModel(ex.Message));
            }
        }
    }
}