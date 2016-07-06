using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data.Model;

namespace Winxuan.Infrastructure
{
    public class UserLoginCache
    {
        private static IDictionary<string, UserInfo> store = new Dictionary<string, UserInfo>();

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

            store.Add(token, new UserInfo(token, user));

            return true;
        }

        /// <summary>
        /// Find user cache data.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static UserInfo FindUser(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new Exception("Token can not be empty.");
            if (!store.ContainsKey(token))
                return new UserInfo();
            return store[token];
        }
    }


    public class UserInfo
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
        public UserInfo()
        {
            this.ID = 0;
            this.CacheDay = DateTime.Now.ToUniversalTime();
        }

        public UserInfo(string token, User user)
            : this()
        {
            this.ID = user.ID;
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
