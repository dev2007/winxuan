using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Winxuan.Infrastructure.Filters;
using Winxuan.Service.Impl;
using Winxuan.Service.Interfaces;

namespace Winxuan.WebApi.Controllers
{
    [UserAuthorize]
    public class TeamController : BaseApiController
    {
        private ITeamService serivce = new TeamService(context);
        /// <summary>
        /// Get user's teams.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Get(int id)
        {
            return await serivce.GetUserTeams(id);
        }
    }
}
