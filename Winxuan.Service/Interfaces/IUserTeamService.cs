using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.Service.Interfaces
{
    /// <summary>
    /// User team service interface.
    /// </summary>
    public interface IUserTeamService
    {
        /// <summary>
        /// Get user teams by user id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns></returns>
        Task<string> GetTeams(int userId);
    }
}
