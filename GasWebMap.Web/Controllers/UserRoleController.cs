using System.Web.Mvc;

namespace GasWebMap.Web.Controllers
{
    [Authorize]
    public class UserRoleController : Controller
    {
        //
        // GET: /UserRole/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View("AddRole");
        }

        public ActionResult RoleTree()
        {
            return View("Roletree");
        }
    }
}