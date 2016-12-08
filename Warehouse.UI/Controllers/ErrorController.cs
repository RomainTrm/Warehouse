using System.Web.Mvc;
using Warehouse.UI.Models;

namespace Warehouse.UI.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        public ActionResult Error(ErrorViewModel errorViewModel)
        {
            this.ViewData.Model = errorViewModel;
            return this.View();
        }

        public ActionResult Back()
        {
            return this.RedirectToRoute(this.Request.UrlReferrer);
        }
    }
}