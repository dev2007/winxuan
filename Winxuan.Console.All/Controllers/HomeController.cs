using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Winxuan.Console.All.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //ViewBag.User = service.Login("123", "123");
            return View();
        }

    }
}
