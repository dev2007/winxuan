using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Winxuan.Data;
using Winxuan.Infrastructure.DTO;
using Winxuan.Service.Impl;
using Winxuan.Service.Interfaces;

namespace Winxuan.WebApi.Controllers
{
    public class RegisteController :  BaseApiController
    {
        private IAccountService service = new AccountService(context);
        public async Task<string> Post([FromBody]RegisteDTO dto)
        {
            return await service.Registe(dto); 
        }
    }
}
