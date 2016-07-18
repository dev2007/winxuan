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
    /// <summary>
    /// Team's users API.
    /// </summary>
    [UserAuthorize]
    public class TeamUserController : BaseApiController
    {
        private ITeamUserService service = new TeamUserService(context);

        /// <summary>
        /// Get users of the team.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Get(int id)
        {
            return await service.GetUsers(id);
        }

        /// <summary>
        /// Add a user into the team.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> Post([FromBody]UserTeamDTO dto)
        {
            return await service.AddUser(dto.UserId, dto.TeamId);
        }

        /// <summary>
        /// Update user's information in the team.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> Put([FromBody]UserTeamDTO dto)
        {
            return await service.UpdateUser(dto);
        }

        /// <summary>
        /// Remove user from the team.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> Delete([FromBody]UserTeamDTO dto)
        {
            return await service.DeleteUser(dto.UserId, dto.TeamId);
        }
    }
}
