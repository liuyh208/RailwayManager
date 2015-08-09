using System.Web.Mvc;

namespace GasWebMap.Web.Controllers
{
    public class LocationController : Controller
    {
        //
        // GET: /Location/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Location/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }
    }
}