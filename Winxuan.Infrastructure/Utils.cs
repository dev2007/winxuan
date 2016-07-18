using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.Infrastructure
{
    /// <summary>
    /// Helper class.
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Get time stamp now.
        /// </summary>
        /// <returns></returns>
        public static long TimeStamp()
        {
            TimeSpan span = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(span.TotalSeconds);
        }

        /// <summary>
        /// Charge the timestamp is bigger than now.
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static bool BiggerNow(long timeStamp)
        {
            return timeStamp > TimeStamp();
        }

        /// <summary>
        /// Convert text to md5 text.
        /// </summary>
        /// <param name="sourceTxt"></param>
        /// <returns></returns>
        public static string MD5(string sourceStr)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Unicode.GetBytes(sourceStr));
            string str = BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.Unicode.GetBytes(sourceStr)));
            return str.Replace("-", "");
        }

        /// <summary>
        /// Get login user's token.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static string LoginToken(string userName, string timeStamp)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            if (string.IsNullOrEmpty(timeStamp))
                throw new ArgumentNullException("timeStamp");

            return Utils.MD5(string.Format("{0}-{1}", userName, timeStamp));
        }

        /// <summary>
        /// Convert text to md5 text and compare with source md5 text.
        /// </summary>
        /// <param name="sourceMD5"></param>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public static bool CompareMD5(string sourceMD5, string sourceStr)
        {
            return MD5(sourceStr) == sourceMD5;
        }
    }
}
