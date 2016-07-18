using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.Server
{
    public class WebUtils
    {
        /// <summary>
        /// Get the authorize token in http requset.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetAuthToken(HttpRequestMessage request)
        {
            IEnumerable<string> headerValues = new List<string>();
            string authToken = string.Empty;
            if (request.Headers.TryGetValues("AuthToken", out headerValues))
            {
                authToken = headerValues.ElementAt(0);
            }

            return authToken;
        }

        /// <summary>
        /// Get current user id.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static int CurrentUserId(HttpRequestMessage request)
        {
            return UserLoginCache.CurrentUserId(GetAuthToken(request));
        }
    }
}
