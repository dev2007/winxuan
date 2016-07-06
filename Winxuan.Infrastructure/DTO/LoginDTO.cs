using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.Infrastructure.DTO
{
    /// <summary>
    /// Login data object.
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// user name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// time stamp.
        /// </summary>
        public string TimeStamp { get; set; }
        /// <summary>
        /// client type.just text description.
        /// </summary>
        public string ClientType { get; set; }
        /// <summary>
        /// token for identity request.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// token for authorize success.The default value is empty.
        /// </summary>
        public string AuthToken { get; set; }
    }
}
