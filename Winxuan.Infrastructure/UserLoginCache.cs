using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data.Model;

namespace Winxuan.Infrastructure
{
    /// <summary>
    /// Cache system for login user.
    /// </summary>
    public class UserLoginCache
    {
        private static IDictionary<string, LoginUserInfo> store = new Dictionary<string, LoginUserInfo>();

        /// <summary>
        /// Add user cache data.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool AddUserCache(string token, User user)
        {
            if (store.ContainsKey(token))
                return false;

            store.Add(token, new LoginUserInfo(token, user));

            return true;
        }

        /// <summary>
        /// Charge if this token is contained.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ContainsKey(string token)
        {
            return store.ContainsKey(token);
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
            if (store.ContainsKey(token))
                return store.Remove(token);
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
                throw new Exception("Token can not be empty.");
            if (!store.ContainsKey(token))
                return new LoginUserInfo();
            return store[token];
        }
    }

    /// <summary>
    /// Basic information class for login users.
    /// </summary>
    public class LoginUserInfo
    {
        /// <summary>
        /// User id.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// User name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User real name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// authorize token.
        /// </summary>
        public string AuthToken { get; set; }
        /// <summary>
        /// Token expiry day.
        /// </summary>
        public DateTime CacheDay { get; set; }
        /// <summary>
        /// Cache day.
        /// </summary>
        private readonly int ExpiryDay = 7;
        public LoginUserInfo()
        {
            this.ID = 0;
            this.CacheDay = DateTime.Now.ToUniversalTime();
        }

        public LoginUserInfo(string token, User user)
            : this()
        {
            this.ID = user.Id;
            this.UserName = user.UserName;
            this.Name = user.Name;

            this.AuthToken = token;
        }

        /// <summary>
        /// Charge if cache is out of time.
        /// </summary>
        /// <returns></returns>
        public bool OutTime()
        {
            return CacheDay.AddDays(ExpiryDay) <= DateTime.Now.ToUniversalTime();
        }
    }
}
