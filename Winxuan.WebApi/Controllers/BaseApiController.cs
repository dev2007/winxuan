using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Winxuan.Data;

namespace Winxuan.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected static readonly WxBaseContext context = new WxSLContext();

        public BaseApiController()
        {

        }
    }
}