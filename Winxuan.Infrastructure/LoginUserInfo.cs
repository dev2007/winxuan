using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data.Model;

namespace Winxuan.Infrastructure
{
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
