using System.Web.Security;
using GasWebMap.Services;
using System.Web.Mvc;
using GasWebMap.Services.Host;

namespace GasWebMap.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
           
         
            string userName = ServiceInit.IsLogin();
            if (!string.IsNullOrEmpty(userName))
            {
                FormsAuthentication.SetAuthCookie(userName,false);
                ViewBag.IsAdmin = ServiceInit.HasRole("管理员");

                ViewBag.ShowEdit = ServiceInit.HasRole("车间管理员") || ServiceInit.HasRole("管理员");
                ViewBag.UserName = userName;
                ViewBag.MenuID = ViewBag.IsAdmin ? 1 : -1;
                return View();
            }
            else
                return RedirectToAction("Index", "Login"); 
        }


        public ActionResult TestSms()
        {
            return View("TestSms");
        }
    }
}