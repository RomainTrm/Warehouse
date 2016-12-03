using System.Web.Mvc;
using System.Web.Routing;
using Warehouse.DataAccess.Events;
using Warehouse.DataAccess.ReadModels;
using Warehouse.Domain;

namespace Warehouse.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Bootstrapper.Init(new EventStoreFake(), new GenericReadModelRepositoryFake());
        }
    }
}
