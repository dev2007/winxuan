using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Winxuan.Data;
using Winxuan.Infrastructure.DTO;
using Winxuan.Service;
using Winxuan.Service.Impl;
using Winxuan.Service.Interfaces;

namespace Winxuan.WebApi.Controllers
{
    public class LoginController : ApiController
    {
        private IAccountService service = new AccountService(new WxContext());
        public async Task<string> Post([FromBody]LoginDTO model)
        {
            return await service.Login(model);
        }
    }
}
