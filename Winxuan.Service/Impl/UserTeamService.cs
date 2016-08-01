using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;
using Winxuan.Service.Interfaces;

namespace Winxuan.Service.Impl
{
    /// <summary>
    /// User team service class.
    /// </summary>
    public class UserTeamService : BaseService, IUserTeamService
    {
        public UserTeamService(WxBaseContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get user teams by user id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns></returns>
        public Task<string> GetTeams(int userId)
        {
            return Task.Run(() =>
            {
                var result = from teamWithUser in context.TeamWithUsers.ToList()
                             join team in context.Teams.ToList() on teamWithUser.TeamId equals team.Id
                             join role in context.UserRoles.ToList() on teamWithUser.UserRole equals role.Id
                             where teamWithUser.UserId == userId
                             select new UserTeamDTO
                             {
                                 UserId = teamWithUser.UserId,
                                 TeamId = teamWithUser.TeamId,
                                 TeamName = team.TeamName,
                                 TeamDescription = team.TeamDescription,
                                 RoleId = teamWithUser.UserRole,
                                 RoleDescription = role.RoleDescription
                             };
                return ResponseSuccess.Json(result);
            });
        }
    }
}
