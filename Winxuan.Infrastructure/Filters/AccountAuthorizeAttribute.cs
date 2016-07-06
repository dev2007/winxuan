using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Winxuan.Infrastructure.Filters
{
    public class AccountAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// the redirect login url.The default value "/Account/Login".
        /// </summary>
        public string LoginUrl { get; set; }
        public AccountAuthorizeAttribute()
        {
            this.LoginUrl = "/Account/Login";
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;
            long userId = 0;
            if(httpContext.Request.HttpMethod == "POST")
            {
                userId = Convert.ToInt64(httpContext.Request.Form["userid"]);
            }
            else if(httpContext.Request.HttpMethod == "GET")
            {
                userId = Convert.ToInt64(httpContext.Request.QueryString["userid"]);
            }

            if (LoginSys.IsLogin(userId))
            {
                result = true;
            }
            else
            {
                httpContext.Response.StatusCode = 401;
                result = false;
            }

            return result;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Response.StatusCode == 401)
            {
                filterContext.HttpContext.Response.Redirect(LoginUrl);
            }
        }
    }
}