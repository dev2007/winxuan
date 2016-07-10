using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        /// <summary>
        /// Get user's teams.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get team's information.
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public Task<string> GetTeam(int teamId)
        {
            return Task.Run(() =>
                {
                    var result = from team in context.Teams.ToList()
                                 join teamuser in context.TeamWithUsers.ToList() on team.Id equals teamuser.TeamId
                                 join user in context.Users.ToList() on teamuser.UserId equals user.Id
                                 where team.Id == teamId && teamuser.UserRole == 1
                                 select new TeamDTO
                                 {
                                     Id = team.Id,
                                     TeamName = team.TeamName,
                                     TeamDescription = team.TeamDescription,
                                     CreatorId = teamuser.UserId,
                                     CreatorName = user.Name
                                 };
                    return ResponseSuccess.Json(result);
                });
        }

        /// <summary>
        /// Update team's information.
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public Task<string> UpdateTeam(Infrastructure.DTO.TeamDTO team)
        {
            return Task.Run(() =>
                {
                    if (team == null)
                        return ResponseFail.Json("", "数据异常，无法更新");

                    var teamObj = context.Teams.Find(team.Id);
                    teamObj.TeamName = team.TeamName;
                    teamObj.TeamDescription = team.TeamDescription;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return ResponseFail.Json("", e.Message);
                    }

                    return ResponseSuccess.Json("");
                });
        }

        /// <summary>
        /// Delete team.
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public Task<string> DeleteTeam(int teamId)
        {
            return Task.Run(() =>
                {
                    var team = context.Teams.Find(teamId);
                    if (team == null)
                    {
                        return ResponseFail.Json("", "组可能已被删除，刷新确认后重试", 204);
                    }
                    context.Teams.Remove(team);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return ResponseFail.Json("", e.Message);
                    }

                    return ResponseSuccess.Json();
                });
        }

        /// <summary>
        /// create team.
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public Task<string> CreateTeam(TeamDTO team)
        {
            return Task.Run(() =>
                {
                    if (team == null)
                        return ResponseFail.Json("", "数据异常，无法创建组", 403);
                    Team teamObj = new Team()
                    {
                        TeamName = team.TeamName,
                        TeamDescription = team.TeamDescription
                    };
                    context.Teams.Add(teamObj);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return ResponseFail.Json("", e.Message, 403);
                    }

                    TeamWithUser teamWithUser = new TeamWithUser()
                    {
                        TeamId = teamObj.Id,
                        UserId = team.CreatorId,
                        UserRole = 1
                    };

                    context.TeamWithUsers.Add(teamWithUser);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        context.Teams.Remove(teamObj);
                        context.SaveChanges();
                        return ResponseFail.Json("", e.Message);
                    }

                    return ResponseSuccess.Json(team);
                });
        }
    }
}
