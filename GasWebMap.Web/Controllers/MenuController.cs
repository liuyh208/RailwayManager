using System.Web.Mvc;
using GasWebMap.Services;
using GasWebMap.Services.Host;

namespace MvcApplication1.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        //
        // GET: /Menu/

        public ActionResult Index(int id )
        {
            bool bl = ServiceInit.HasRole("管理员"); ;
           var data=  MenuDataService.GetMenus(bl);
            return View(data);
        }
        
    }
}