using System;
using System.Web.Mvc;
using Warehouse.Domain;
using Warehouse.Domain.Commands.Bus;
using Warehouse.Domain.Commands.EnableItem;
using Warehouse.Domain.ReadModels;
using Warehouse.UI.Models;

namespace Warehouse.UI.Controllers
{
    [HandleError]
    public class DisabledItemsController : Controller
    {
        private readonly ICommandBus commandBus;

        public DisabledItemsController()
        {
            this.commandBus = Bootstrapper.CommandBus;
        }

        public ActionResult Enable(Guid id)
        {
            try
            {
                var item = new DisabledItemsListView().GetItem(id);
                this.commandBus.Send(new EnableItemCommand(item.Id));
                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Error", "Error", new ErrorViewModel(ex.Message));
            }
        }
    }
}