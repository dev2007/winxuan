using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data.Model;

namespace Winxuan.Data
{
    public class WxContext : WxBaseContext
    {
        public WxContext()
            : base("WxContext")
        {

        }
    }
}
