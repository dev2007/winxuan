using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.Infrastructure
{
    /// <summary>
    /// Cache for all login users.
    /// </summary>
    public class LoginSys
    {
        private static IDictionary<long, LoginCache> SysCache = new Dictionary<long, LoginCache>();

        public static bool IsLogin(long userId)
        {
            if (SysCache.ContainsKey(userId))
            {
                var cache = SysCache[userId];
                return Utils.BiggerNow(cache.Expiry);
            }
            else
                return false;
        }
    }

    /// <summary>
    /// Cache for login user.
    /// </summary>
    class LoginCache
    {
        public string Token { get; set; }
        public long Expiry { get; set; }
    }
}
