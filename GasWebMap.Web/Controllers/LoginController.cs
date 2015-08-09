using System.Web.Mvc;
using System.Web.Security;
using GasWebMap.Services.Host;

namespace GasWebMap.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }



        //
        // GET: /Login/Create

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            ServiceInit.Logout();
            return RedirectToAction("Index", "Login"); 
        }

    }
}