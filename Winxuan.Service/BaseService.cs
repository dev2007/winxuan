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
        protected WxContext context = null;
        public BaseService(WxContext context)
        {
            this.context = context;
        }
    }
}
