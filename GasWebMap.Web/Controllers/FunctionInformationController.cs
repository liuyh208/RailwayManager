using System.Web.Mvc;

namespace GasWebMap.Web.Controllers
{
    public class FunctionInformationController : Controller
    {
        //
        // GET: /FunctionInformation/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GroupAdd()
        {
            return View("GroupAdd");
        }

        public ActionResult FunctionAdd()
        {
            return View("FunctionAdd");
        }

        public ActionResult GroupUpdate()
        {
            return View("GroupUpdate");
        }
    }
}