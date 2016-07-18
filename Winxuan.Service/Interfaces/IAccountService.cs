using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Service.Interfaces
{
    /// <summary>
    /// Account service interface.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// User login.
        /// </summary>
        /// <param name="dto">Login information.</param>
        /// <returns></returns>
        Task<string> Login(LoginDTO dto);
        /// <summary>
        /// User logout. 
        /// </summary>
        /// <param name="authToken">User's authorized token.</param>
        /// <returns></returns>
        Task<string> Logout(string authToken);
    }
}
