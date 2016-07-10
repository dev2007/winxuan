using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;
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
        /// Get team information.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Get(int id)
        {
            return await serivce.GetTeam(id);
        }

        /// <summary>
        /// Get teams of current user.
        /// </summary>
        /// <returns></returns>
        public async Task<string> Get()
        {
            int id = -1;
            string msg = string.Empty;
            try
            {
                WebUtils.CurrentUserId(Request);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            if (!string.IsNullOrEmpty(msg))
                return await Task.Run(() => ResponseFail.Json("", msg));

            return await serivce.GetUserTeams(id);
        }

        /// <summary>
        /// Create team.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> Post([FromBody]TeamDTO dto)
        {
            return await serivce.CreateTeam(dto);
        }

        /// <summary>
        /// Delete team.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Delete(int id)
        {
            return await serivce.DeleteTeam(id);
        }

        /// <summary>
        /// Update team information.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> Put([FromBody]TeamDTO dto)
        {
            return await serivce.UpdateTeam(dto);
        }
    }
}
