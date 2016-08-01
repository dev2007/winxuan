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
        /// <summary>
        /// Index page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string cookie = GetCookieToken();
            if (!string.IsNullOrEmpty(cookie))
            {
                ResponseJson<LoginUserInfo> result = _Login(new LoginDTO { }, cookie);
                if (!result.Status)
                    return View();

                if (!string.IsNullOrEmpty(result.Data.AuthToken))
                    return View(new UserViewModel { ID = result.Data.ID, Name = result.Data.Name });
                return View();
            }
            return View();
        }

        /// <summary>
        /// Login action.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            model.TimeStamp = Utils.TimeStamp().ToString();
            model.Token = Utils.LoginToken(model.UserName, model.TimeStamp);
            ResponseJson<LoginUserInfo> result = _Login(model);
            //add cookies.
            if (result.Data != null)
            {
                Response.Cookies.Add(new HttpCookie("authtoken", result.Data.AuthToken) { Expires = DateTime.Now.ToUniversalTime().AddDays(7) });
                Session.Add("userid", result.Data.ID);
            }
            return Json(result);
        }

        /// <summary>
        /// Logout action.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            string cookie = GetCookieToken();
            if (!string.IsNullOrEmpty(cookie))
            {
                ResponseJson<object> responseJson = WebUtils.Post<object>(string.Format("{0}/{1}", ApiServer, "api/logout"), new LogoutDTO() { AuthoToken = cookie }, cookie);
                if (responseJson.Status)
                    return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// User registe.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Registe()
        {
            return View();
        }

        /// <summary>
        /// User registe for post form.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Registe")]
        public ActionResult RegisteUser(RegisteDTO model)
        {
            ResponseJson<string> result = WebUtils.Post<string>(string.Format("{0}/{1}", ApiServer, "api/user"), model);
            return Json(result);
        }

        /// <summary>
        /// TODO:find password.
        /// </summary>
        /// <returns></returns>
        public ActionResult FindPwd()
        {
            return View();
        }


        /// <summary>
        /// Login function.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="authToken"></param>
        /// <returns></returns>
        private ResponseJson<LoginUserInfo> _Login(LoginDTO model, string authToken = "")
        {
            ResponseJson<LoginUserInfo> responsJson = WebUtils.Post<LoginUserInfo>(string.Format("{0}/{1}", ApiServer, "api/login"), model, authToken);
            return responsJson;
        }
    }
}
