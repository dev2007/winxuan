using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Service.Interfaces
{
    /// <summary>
    /// Team service interface.
    /// </summary>
    public interface ITeamService
    {
        /// <summary>
        /// Get team data by team id.
        /// </summary>
        /// <param name="teamId">Team's id.</param>
        /// <returns></returns>
        Task<string> GetTeam(int teamId);
        /// <summary>
        /// Get teams data by page index and page size.
        /// </summary>
        /// <param name="page">Query page index.First is 1.</param>
        /// <param name="pageSize">Data size per page.</param>
        /// <returns></returns>
        Task<string> GetTeams(int page, int pageSize = 30);
        /// <summary>
        /// Create new team data.
        /// </summary>
        /// <param name="team">New team's data.</param>
        /// <returns></returns>
        Task<string> CreateTeam(TeamDTO team);
        /// <summary>
        /// Update team data.
        /// </summary>
        /// <param name="team">Team's data.</param>
        /// <returns></returns>
        Task<string> UpdateTeam(TeamDTO team);
        /// <summary>
        /// Delete team data by team's id.
        /// </summary>
        /// <param name="teamId">Team's id.</param>
        /// <returns></returns>
        Task<string> DeleteTeam(int teamId);
    }
}
