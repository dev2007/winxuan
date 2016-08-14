using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winxuan.Infrastructure;

namespace Winxuan.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly static string ApiServer = "http://localhost:64181";//ConfigurationManager.AppSettings["ApiServer"].ToString();
        public BaseController()
        {
        }

        protected string GetCookieToken()
        {
            HttpCookie cookie = Request.Cookies["authtoken"];
            if (cookie != null)
            {
                if (!string.IsNullOrEmpty(cookie.Value))
                {
                    return cookie.Value;
                }
            }
            return string.Empty;
        }
    }
}
