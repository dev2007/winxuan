using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Winxuan.Data;

namespace Winxuan.WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        public IEnumerable<string> Get()
        {
            return context.Users.ToList().Select(t =>
                {
                    return t.Name;
                });
        }

        public string Get(int id)
        {
            return context.Users.Find(id).Name;
        }
    }
}
