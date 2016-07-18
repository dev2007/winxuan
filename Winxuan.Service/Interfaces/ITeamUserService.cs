using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Service.Interfaces
{
    /// <summary>
    /// Team's user service interface.
    /// </summary>
    public interface ITeamUserService
    {
        /// <summary>
        /// Get team users by team id.
        /// </summary>
        /// <param name="teamId">Team id.</param>
        /// <returns></returns>
        Task<string> GetUsers(int teamId);
        /// <summary>
        /// Add user into the team.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="teamId">Team id.</param>
        /// <returns></returns>
        Task<string> AddUser(int userId, int teamId);
        /// <summary>
        /// Update user data in the team.
        /// </summary>
        /// <param name="dto">Data to update.</param>
        /// <returns></returns>
        Task<string> UpdateUser(UserTeamDTO dto);
        /// <summary>
        /// Delete user from team.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="teamId">Team id.</param>
        /// <returns></returns>
        Task<string> DeleteUser(int userId, int teamId);
    }
}
