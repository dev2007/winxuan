using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Winxuan.Infrastructure.DTO;
using Winxuan.Service.Impl;
using Winxuan.Service.Interfaces;

namespace Winxuan.WebApi.Controllers
{
    public class LogoutController:BaseApiController
    {
        private IAccountService service = new AccountService(context);

        public async Task<string> Post(LogoutDTO dto)
        {
            return await service.Logout(dto.AuthoToken); 
        }
    }
}