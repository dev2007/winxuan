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
    /// <summary>
    /// Team service class.
    /// </summary>
    public class TeamService : BaseService, ITeamService
    {
        public TeamService(WxBaseContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get team data by team id.
        /// </summary>
        /// <param name="teamId">Team's id.</param>
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
                    
                    if(result.Count() == 0)
                    {
                        return ResponseFail.NoContent();
                    }

                    if(result.Count() > 1)
                    {
                        return ResponseFail.ExpectationFailed(message: "数据异常，非唯一，无法操作");
                    }

                    return ResponseSuccess.Json(result.First());
                });
        }

        /// <summary>
        /// Update team data.
        /// </summary>
        /// <param name="team">Team's data.</param>
        /// <returns></returns>
        public Task<string> UpdateTeam(Infrastructure.DTO.TeamDTO team)
        {
            return Task.Run(() =>
                {
                    if (team == null)
                        return ResponseFail.Json("", "数据异常，无法更新");

                    var teamObj = context.Teams.ToList().Find(t => t.Id == team.Id);

                    if(teamObj == null)
                    {
                        return ResponseFail.NoContent();
                    }

                    teamObj.TeamName = team.TeamName;
                    teamObj.TeamDescription = team.TeamDescription;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return ResponseFail.ExpectationFailed(message: e.Message);
                    }

                    return ResponseSuccess.Json("");
                });
        }

        /// <summary>
        /// Delete team data by team's id.
        /// </summary>
        /// <param name="teamId">Team's id.</param>
        /// <returns></returns>
        public Task<string> DeleteTeam(int teamId)
        {
            return Task.Run(() =>
                {
                    var team = context.Teams.ToList().Find(t => t.Id == teamId);
                    if (team == null)
                    {
                        return ResponseFail.NoContent("");
                    }
                    context.Teams.Remove(team);
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
        /// Create new team data.
        /// </summary>
        /// <param name="team">New team's data.</param>
        /// <returns></returns>
        public Task<string> CreateTeam(TeamDTO team)
        {
            return Task.Run(() =>
                {
                    if (team == null)
                        return ResponseFail.Json("", "数据异常，无法创建组");
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
                        return ResponseFail.ExpectationFailed(message: e.Message);
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
                        return ResponseFail.ExpectationFailed(message: e.Message);
                    }

                    return ResponseSuccess.Json(team);
                });
        }

        /// <summary>
        /// Get teams data by page index and page size.
        /// </summary>
        /// <param name="page">Query page index.First is 1.</param>
        /// <param name="pageSize">Data size per page.</param>
        /// <returns></returns>
        public Task<string> GetTeams(int page, int pageSize = 30)
        {
            return Task.Run(() =>
                {
                    if(page < 1)
                    {
                        return ResponseFail.ExpectationFailed(message: "页数应该大于等于1");
                    }

                    if(pageSize < 1)
                    {
                        return ResponseFail.ExpectationFailed(message: "每页数量应该为正数");
                    }

                    try
                    {
                        var excludePre = context.Teams.ToList().Take((page - 1) * pageSize);
                        var result = context.Teams.ToList().Except(excludePre).Take(pageSize);
                        return ResponseSuccess.Json(result);
                    }
                    catch (Exception e)
                    {
                        return ResponseFail.ExpectationFailed(message: e.Message);
                    }
                });
        }
    }
}
