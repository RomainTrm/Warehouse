using System;
using System.Linq;
using System.Web.Mvc;
using Warehouse.Domain.ReadModels;
using Warehouse.UI.Models;

namespace Warehouse.UI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            this.ViewData.Model = new ItemsListView();
            return this.View();
        }

        public ActionResult Create()
        {
            this.ViewData.Model = new ItemName();
            return this.View();
        }

        public ActionResult Details(Guid id)
        {
            this.ViewData.Model = new ItemsListView().Items
                                                     .Single(item => Equals(item.Id.Value, id));
            return this.View();
        }
    }
}