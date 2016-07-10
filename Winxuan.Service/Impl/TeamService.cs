using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data;
using Winxuan.Data.Model;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;
using Winxuan.Service.Interfaces;

namespace Winxuan.Service.Impl
{
    public class TeamService : BaseService, ITeamService
    {
        public TeamService(WxBaseContext context)
            : base(context)
        {

        }

        public Task<string> GetUserTeams(int userId)
        {
            return Task.Run(() =>
                {
                    IEnumerable<TeamWithUser> teamWithUserList = context.TeamWithUsers.ToList().Where(t => t.UserId == userId);
                    var result = from teamWithUser in context.TeamWithUsers.ToList()
                                 join team in context.Teams.ToList() on teamWithUser.TeamId equals team.Id
                                 join role in context.UserRoles.ToList() on teamWithUser.UserRole equals role.Id
                                 select new UserTeamDTO
                                 {
                                     UserId = teamWithUser.UserId,
                                     TeamId = teamWithUser.TeamId,
                                     TeamDescription = team.TeamDescription,
                                     RoleId = teamWithUser.UserRole,
                                     RoleDescription = role.RoleDescription
                                 };
                    return ResponseSuccess.Json(result);
                });
        }

        public Task<string> GetTeam(int teamId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateTeam(Infrastructure.DTO.TeamDTO team)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteTeam(Infrastructure.DTO.TeamDTO team)
        {
            throw new NotImplementedException();
        }
    }
}
