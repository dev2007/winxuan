using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Winxuan.Data;
using Winxuan.Server.Filters;
using Winxuan.Service.Impl;
using Winxuan.Service.Interfaces;

namespace Winxuan.WebApi.Controllers
{
    /// <summary>
    /// user's teams api.
    /// </summary>
    [UserAuthorize]
    public class UserTeamController : BaseApiController
    {
        private IUserTeamService service = new UserTeamService(context);

        /// <summary>
        /// Get user's teams.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [RoleAuthorize]
        public async Task<string> Get(int id)
        {
            return await service.GetTeams(id);
        }
    }
}
