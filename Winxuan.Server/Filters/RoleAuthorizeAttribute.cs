using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Winxuan.Infrastructure;


namespace Winxuan.Server.Filters
{
    /// <summary>
    /// Role authorize filter.Query the user's data by the user or admin.
    /// </summary>
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// check if user has login.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //TODO 开发阶段关闭校验
            string authToken = WebUtils.GetAuthToken(actionContext.Request);
            var userData = UserLoginCache.FindUser(authToken);
            //admin has operation right.
            if (userData.UserName == "admin")
                return;
            var idPair = actionContext.ActionArguments.First(t => t.Key == "id");
            if(string.IsNullOrEmpty(idPair.Key))
            {
                Restrict(actionContext);
            }
            else
            {
                var id = idPair.Value;
                //if query param id is not the authorize token id,the user has not operation right.
                if (Convert.ToInt32(id) != userData.ID)
                    Restrict(actionContext);
            }
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

    /// <summary>
    /// Params type for RoleAuthorize.
    /// </summary>
    public enum ParamType
    {
        USER = 1,
        TEAM = 2
    }
}
