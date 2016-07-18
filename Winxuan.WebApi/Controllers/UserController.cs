using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Winxuan.Infrastructure.DTO;
using Winxuan.Server.Filters;
using Winxuan.Service.Impl;
using Winxuan.Service.Interfaces;

namespace Winxuan.WebApi.Controllers
{
    [UserAuthorize]
    public class UserController : BaseApiController
    {
        private IUserService service = new UserService(context);
        
        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> Post([FromBody]RegisteDTO dto)
        {
            return await service.CreateUser(dto);
        }
    }
}
