using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Winxuan.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly static string ApiServer = ConfigurationManager.AppSettings["ApiServer"].ToString();

        public BaseController()
        {
        }
    }
}
