using System.Web.Mvc;
using ThreeRoads.Data.Contracts;
using ThreeRoads.Data.SqlServerCe;

namespace ThreeRoads.MVC.Controllers
{
    public class BaseController: Controller
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        public BaseController()
        {
            UnitOfWork = new ThreeRoadsDb();
        }

        public BaseController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

    }
}
