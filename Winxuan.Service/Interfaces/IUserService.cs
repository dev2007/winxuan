using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Service.Interfaces
{
    /// <summary>
    /// User service interface.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get user data by user's id.
        /// </summary>
        /// <param name="userId">User's id.</param>
        /// <returns></returns>
        Task<string> GetUser(int userId);
        /// <summary>
        /// Get users by page index and page size.
        /// </summary>
        /// <param name="page">Query page index.First is 1.</param>
        /// <param name="pageSize">Data size per page.</param>
        /// <returns></returns>
        Task<string> GetUsers(int page, int pageSize = 30);
        /// <summary>
        /// Create a new user data.
        /// </summary>
        /// <param name="dto">New user's data.</param>
        /// <returns></returns>
        Task<string> CreateUser(RegisteDTO dto);
        /// <summary>
        /// Update user's data.
        /// </summary>
        /// <param name="dto">User's data.</param>
        /// <returns></returns>
        Task<string> UpdateUser(UserDTO dto);
        /// <summary>
        /// Delete user by user id.
        /// </summary>
        /// <param name="userId">User's id.</param>
        /// <returns></returns>
        Task<string> DeleteUser(int userId);
    }
}
