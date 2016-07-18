using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data;

namespace Winxuan.Service
{
    /// <summary>
    /// Base class for service.
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// Data source context object.
        /// </summary>
        protected WxBaseContext context = null;
        public BaseService(WxBaseContext context)
        {
            this.context = context;
        }
    }
}
