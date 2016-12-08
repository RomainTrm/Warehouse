using System;
using System.Web.Mvc;
using Warehouse.Domain;
using Warehouse.Domain.Commands.Bus;
using Warehouse.Domain.Commands.CreateItem;
using Warehouse.UI.Models;

namespace Warehouse.UI.Controllers
{
    [HandleError]
    public class CreateController : Controller
    {
        private readonly ICommandBus commandBus;

        public CreateController()
        {
            this.commandBus = Bootstrapper.CommandBus;
        }

        [HttpPost]
        public ActionResult Create(ItemNameViewModel itemName)
        {
            try
            {
                this.commandBus.Send(new CreateItemCommand(itemName.Value));
                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Error", "Error", new ErrorViewModel(ex.Message));
            }
        }
    }
}