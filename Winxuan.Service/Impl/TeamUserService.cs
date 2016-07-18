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
    /// <summary>
    /// Team user service class.
    /// </summary>
    public class TeamUserService : BaseService, ITeamUserService
    {
        public TeamUserService(WxBaseContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get team users by team id.
        /// </summary>
        /// <param name="teamId">Team id.</param>
        /// <returns></returns>
        public Task<string> GetUsers(int teamId)
        {
            return Task.Run(() =>
            {
                var result = from teamWithUsers in context.TeamWithUsers.ToList()
                             join teams in context.Teams.ToList() on teamWithUsers.TeamId equals teams.Id
                             join users in context.Users.ToList() on teamWithUsers.UserId equals users.Id
                             join roles in context.UserRoles.ToList() on teamWithUsers.UserRole equals roles.Id
                             where teamWithUsers.TeamId == teamId
                             select new UserTeamDTO
                             {
                                 TeamId = teamId,
                                 TeamName = teams.TeamName,
                                 TeamDescription = teams.TeamDescription,
                                 UserId = users.Id,
                                 UserName = users.Name,
                                 RoleId = teamWithUsers.UserRole,
                                 RoleDescription = roles.RoleDescription
                             };

                if (result.Count() == 0)
                    return ResponseFail.NoContent();

                return ResponseSuccess.Json(result);
            });
        }

        /// <summary>
        /// Add user into the team.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="teamId">Team id.</param>
        /// <returns></returns>
        public Task<string> AddUser(int userId, int teamId)
        {
            return Task.Run(() =>
                {
                    var user = context.Users.ToList().Find(t => t.Id == userId);
                    if (user == null)
                        return ResponseFail.Json("", "无相应用户，操作失败");
                    var team = context.TeamWithUsers.ToList().Where(t => t.TeamId == teamId);
                    if (team == null)
                    {
                        return ResponseFail.Json("", "无相应组，操作失败");
                    }

                    if (team.Count() == 0)
                    {
                        return ResponseFail.Json("", "无相应组，操作失败");
                    }

                    var teamUser = new TeamWithUser()
                    {
                        TeamId = teamId,
                        UserId = userId,
                        UserRole = 3
                    };

                    context.TeamWithUsers.Add(teamUser);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return ResponseFail.ExpectationFailed(message: e.Message);
                    }

                    return ResponseSuccess.Json();
                });
        }

        /// <summary>
        /// Update user data in the team.
        /// </summary>
        /// <param name="dto">Data to update.</param>
        /// <returns></returns>
        public Task<string> UpdateUser(Infrastructure.DTO.UserTeamDTO dto)
        {
            return Task.Run(() =>
                {
                    try
                    {
                        var teamWithUser = context.TeamWithUsers.First(t => t.TeamId == dto.TeamId && t.UserId == dto.UserId);
                        if (teamWithUser == null)
                            return ResponseFail.NoContent();

                        var role = context.UserRoles.Where(t => t.Id == dto.RoleId);
                        if (role.Count() == 0)
                            return ResponseFail.ExpectationFailed(message: "用户角色值异常");

                        teamWithUser.UserRole = dto.RoleId;

                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return ResponseFail.ExpectationFailed(message: e.Message);
                    }

                    return ResponseSuccess.Json();
                });
        }

        /// <summary>
        /// Delete user from team.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="teamId">Team id.</param>
        /// <returns></returns>
        public Task<string> DeleteUser(int userId, int teamId)
        {
            return Task.Run(() =>
                {
                    var teamWithUser = context.TeamWithUsers.First(t => t.TeamId == teamId && t.UserId == userId);
                    if (teamWithUser == null)
                        return ResponseFail.NoContent();

                    context.TeamWithUsers.Remove(teamWithUser);
                    try
                    {
                        context.SaveChanges();
                    }   catch(Exception e)
                    {
                        return ResponseFail.ExpectationFailed(message: e.Message);
                    }

                    return ResponseSuccess.Json();
                });
        }
    }
}
