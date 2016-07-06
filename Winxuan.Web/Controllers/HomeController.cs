using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;
using Winxuan.Web.Models;

namespace Winxuan.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["authtoken"];
            if (cookie != null)
            {
                if (cookie.Expires <= DateTime.Now.ToUniversalTime() && !string.IsNullOrEmpty(cookie.Value))
                {
                    ResponseJson<UserInfo> result = _Login(new LoginDTO() { AuthToken = cookie.Value });
                    return View(new UserViewModel { ID = result.Data.ID, Name = result.Data.Name });
                }
            }
            return View();
        }

        public ActionResult Login(LoginDTO model)
        {
            model.TimeStamp = Utils.TimeStamp().ToString();
            model.Token = Utils.MD5(string.Format("{0}-{1}", model.UserName, model.TimeStamp));
            ResponseJson<UserInfo> result = _Login(model);
            //add cookies.
            if (result.Data != null)
                Response.Cookies.Add(new HttpCookie("authtoken", result.Data.AuthToken) { Expires = DateTime.Now.ToUniversalTime().AddDays(7) });
            return Json(result);
        }

        private ResponseJson<UserInfo> _Login(LoginDTO model)
        {
            ResponseJson<UserInfo> responsJson = WebUtils.Post<UserInfo>(string.Format("{0}/{1}", ApiServer, "api/login"), model);
            return responsJson;
        }
    }
}
