using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;
using Winxuan.Server.Filters;
using Winxuan.Service.Impl;
using Winxuan.Service.Interfaces;

namespace Winxuan.WebApi.Controllers
{
    [UserAuthorize]
    public class TeamController : BaseApiController
    {
        private ITeamService serivce = new TeamService(context);
        /// <summary>
        /// Get the team data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Get(int id)
        {
            return await serivce.GetTeam(id);
        }

        /// <summary>
        /// Get all teams data.
        /// </summary>
        /// <returns></returns>
        public async Task<string> Get()
        {
            //TODO:读取header中的分页数据。
            return await serivce.GetTeams(1);
        }

        /// <summary>
        /// Create a team.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> Post([FromBody]TeamDTO dto)
        {
            return await serivce.CreateTeam(dto);
        }

        /// <summary>
        /// Delete the team.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Delete(int id)
        {
            return await serivce.DeleteTeam(id);
        }

        /// <summary>
        /// Update the team data.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> Put([FromBody]TeamDTO dto)
        {
            return await serivce.UpdateTeam(dto);
        }
    }
}
