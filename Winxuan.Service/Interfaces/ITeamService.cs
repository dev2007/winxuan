using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Service.Interfaces
{
    public interface ITeamService
    {
        Task<string> GetUserTeams(int userId);
        Task<string> GetTeam(int teamId);
        Task<string> CreateTeam(TeamDTO team);
        Task<string> UpdateTeam(TeamDTO team);
        Task<string> DeleteTeam(int teamId);
    }
}
