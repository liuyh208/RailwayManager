using System.Web.Mvc;

namespace GasWebMap.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Manager()
        {
            return View("Manager");
        }

        public ActionResult UserAdd()
        {
            return View("UserAdd");
        }
    }
}