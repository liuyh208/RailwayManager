using System.Web.Mvc;

namespace GasWebMap.Web.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DepartmentAdd()
        {
            return View("DepartmentAdd");
        }

        public ActionResult DepartmentUpdate()
        {
            return View("DepartmentUpdate");
        }
    }
}