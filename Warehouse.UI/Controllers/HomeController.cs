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
            return this.View("Create", new ItemName());
        }
    }
}