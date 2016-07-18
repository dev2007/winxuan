using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data.Model;
using Winxuan.Infrastructure;

namespace Winxuan.Server
{
    /// <summary>
    /// Cache system for login user.
    /// </summary>
    public class UserLoginCache
    {
        private static IDictionary<string, LoginUserInfo> tokenStore = new Dictionary<string, LoginUserInfo>();

        /// <summary>
        /// Add user cache data.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool AddUserCache(string token, User user, IList<Team> teamList = null)
        {
            if (tokenStore.ContainsKey(token))
                return false;

            var userInfo = new LoginUserInfo(token, user);
            tokenStore.Add(token, userInfo);

            return true;
        }

        /// <summary>
        /// Update user's cache.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UpdateUserCache(string token, User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (!tokenStore.ContainsKey(token))
                return false;

            var oldUser = tokenStore[token];
            oldUser.Name = user.Name;

            return true;
        }

        /// <summary>
        /// Charge if this token is contained.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ContainsKey(string token)
        {
            return tokenStore.ContainsKey(token);
        }

        /// <summary>
        /// Charge if the user hold the token is login.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsLogin(string token)
        {
            if (ContainsKey(token))
            {
                return !FindUser(token).OutTime();
            }
            else
                return false;
        }

        /// <summary>
        /// Remove login user cache by authtoken.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool RemoveCache(string token)
        {
            if (tokenStore.ContainsKey(token))
            {
                return tokenStore.Remove(token);
            }

            return true;
        }

        /// <summary>
        /// Find user cache data.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static LoginUserInfo FindUser(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("token");
            if (!tokenStore.ContainsKey(token))
                return new LoginUserInfo();
            return tokenStore[token];
        }

        /// <summary>
        /// Get user id by login token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int CurrentUserId(string token)
        {
            LoginUserInfo info = FindUser(token);
            return info.ID;
        }
    }

   
}
