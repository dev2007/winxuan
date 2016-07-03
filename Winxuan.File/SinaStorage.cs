using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.File
{
    public class SinaStorage
    {
    }

    /// <summary>
    /// Sina site.
    /// </summary>
    public sealed class SinaSite
    {
        /// <summary>
        /// First choice site.
        /// </summary>
        public static readonly string FIRST_CHOICE = "sinacloud.net";
        /// <summary>
        /// Second choice site.
        /// </summary>
        public static readonly string SECOND_CHOIC = "sinastorage.cn";
        /// <summary>
        /// CDN download site.
        /// </summary>
        public static readonly string CDN_DOWNLOAD = "cdn.sinacloud.net";
        /// <summary>
        /// CDN upload site.
        /// </summary>
        public static readonly string CDN_UPLOAD = "up.sinacloud.net";
    }
}
