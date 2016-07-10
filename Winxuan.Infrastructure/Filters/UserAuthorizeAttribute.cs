using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Winxuan.Infrastructure.Filters
{
    public class UserAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// check if user has login.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //TODO 开发阶段关闭校验
            //string authToken = WebUtils.GetAuthToken(actionContext.Request); 

            //if (string.IsNullOrEmpty(authToken))
            //{
            //    Restrict(actionContext);
            //}
            //else if (!UserLoginCache.IsLogin(authToken))
            //{
            //    Restrict(actionContext);
            //}
        }

        /// <summary>
        /// Authorize fail.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private void Restrict(HttpActionContext httpContext)
        {
            httpContext.Response = new HttpResponseMessage();
            httpContext.Response.StatusCode = HttpStatusCode.Unauthorized;

            httpContext.Response.Content = new StringContent(ResponseFail.Json("", "非授权用户，无权调用接口"));
        }
    }
}
